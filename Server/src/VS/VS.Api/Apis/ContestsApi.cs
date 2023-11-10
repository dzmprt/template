using Common.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VS.Application.Handler.Contests.DTOs;
using VS.Application.Handler.Contests.Queries.GetContest;

namespace VS.Api.Apis;

public class ContestsApi: IApi
{
    public void Register(WebApplication app, string baseApiUrl = null)
    {
        #region Queries
        
        app.MapGet(baseApiUrl + "/Contests/{id}", async (
                    [FromRoute] int id,
                    IMediator mediator,
                    CancellationToken cancellationToken) =>
                Results.Ok(await mediator.Send(new GetContestQuery { Id = id }, cancellationToken)))
            .Produces<ContestInfoDto>()
            .WithDescription("Get contest")
            .WithTags("Contests");
        
        #endregion
    }
}