using MediatR;
using VS.Domain;

namespace VS.Application.Handler.Images.Queries.GetImageFileQuery;

public class GetImageQuery : IRequest<byte[]>
{
    public int Id { get; set; }
    
    public bool? Lite { get; set; }
}