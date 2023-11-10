using Common.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VS.Application.Handler.Images.Queries.GetImageFileQuery;

namespace VS.Api.Apis;

public class ImagesApi : IApi
{
    public void Register(WebApplication app, string baseApiUrl = null)
    {
        #region Queries

        app.MapGet(baseApiUrl + "/Images/Lite/{id}_lite.jpeg", async (
                    [FromRoute] int id,
                    IMediator mediator,
                    CancellationToken cancellationToken) =>
                Results.File(
                    await mediator.Send(new GetImageQuery { Id = id, Lite = true }, cancellationToken),
                    "image/jpeg"
                ))
            .Produces<FileResult>()
            .WithDescription("Get image")
            .WithTags("Images");


        app.MapGet(baseApiUrl + "/Images/{id}.jpeg", async (
                    [FromRoute] int id,
                    IMediator mediator,
                    CancellationToken cancellationToken) =>
                Results.File(await mediator.Send(new GetImageQuery { Id = id }, cancellationToken), "image/jpeg"
                ))
            .Produces<FileResult>()
            .WithDescription("Get image")
            .WithTags("Images");

        #endregion
    }
}