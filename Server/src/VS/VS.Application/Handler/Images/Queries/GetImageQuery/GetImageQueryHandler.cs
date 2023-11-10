using Common.Application.Abstractions.Persistence.Repository.Read;
using Common.Application.Exceptions;
using MediatR;
using VS.Application.Utils;
using VS.Domain;
using VS.Domain.FC;

namespace VS.Application.Handler.Images.Queries.GetImageQuery;

public class GetImageQueryHandler : IRequestHandler<GetImageFileQuery.GetImageQuery, byte[]>
{
    private readonly IBaseReadRepository<ParticipantImage> _participantImages;
    private readonly IBaseReadRepository<Domain.Image> _images;

    private readonly IBaseReadRepository<BlobFile> _blobFiles;

    public GetImageQueryHandler(
        IBaseReadRepository<ParticipantImage> participantImages, 
        IBaseReadRepository<Domain.Image> images,
        IBaseReadRepository<BlobFile> blobFiles)
    {
        _participantImages = participantImages;
        _images = images;
        _blobFiles = blobFiles;
    }

    public async Task<byte[]> Handle(GetImageFileQuery.GetImageQuery request, CancellationToken cancellationToken)
    {
        var path = $"Files/{request.Id}{(request.Lite.GetValueOrDefault() ? "_lite" : "")}.jpeg";

        byte[] date;
        if (!File.Exists(path))
        {
            var participantImage =
                await _participantImages.SingleOrDefaultAsync(i => i.ImageId == request.Id, cancellationToken);
            if (participantImage is null)
            {
                throw new NotFoundException(request);
            }
            var image = await _images.SingleAsync(i => i.Id == participantImage.ImageId, cancellationToken);
            var blobFile = await _blobFiles.SingleAsync(b => b.Id == image.BlobFileId, cancellationToken);
            using var img = new ImageUtil(blobFile.Data);

            if (request.Lite.GetValueOrDefault())
            {
                if (img.Width > 350)
                {
                    double hCof = (double)img.Width / 350;
                    img.Resize((int)(img.Height / hCof), 350);
                }

                date = img.ToJpeg(70);
            }
            else
            {
                date = img.ToJpeg();
            }

            FileInfo file = new FileInfo(path);
            if (!file.Directory!.Exists)
            {
                file.Directory.Create();
            }
            await File.WriteAllBytesAsync(path, date, cancellationToken);
        }
        else
        {
            date = await File.ReadAllBytesAsync(path, cancellationToken);
        }

        return date;
    }
}