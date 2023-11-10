using Common.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VS.Application.Handler.Contests.DTOs;
using VS.Application.Handler.VotesSet.Commands.AddOrUpdate;
using VS.Application.Handler.VotesSet.DTOs;
using VS.Application.Handler.VotesSet.Queries.GetVotesSet;

namespace VS.Api.Apis;

public class VoteSetApi : IApi
{
    public void Register(WebApplication app, string baseApiUrl = null)
    {
        #region Queries

        app.MapGet(baseApiUrl + "/VoteSets/{contestId}/{ticket}", async (
                    [FromRoute] int contestId,
                    [FromRoute] string ticket,
                    IMediator mediator,
                    CancellationToken cancellationToken) =>
                Results.Ok(await mediator.Send(new VotesSetQuery { Ticket = ticket, ContestId = contestId },
                    cancellationToken)))
            .Produces<VotesSetDto>()
            .WithDescription("Get vote set")
            .WithTags("VoteSets");

        #endregion

        #region Commands

        app.MapPost(baseApiUrl + "/VoteSets", async (
                    [FromBody] AddOrUpdateVoteSetCommand createUserCommand,
                    IMediator mediator,
                    CancellationToken cancellationToken) =>
                Results.Ok(await mediator.Send(createUserCommand, cancellationToken)))
            .Produces<VotesSetDto>()
            .WithDescription("Create vote set")
            .WithTags("VoteSets");

        #endregion
    }
}