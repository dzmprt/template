using MediatR;
using VS.Application.Handler.Contests.DTOs;

namespace VS.Application.Handler.Contests.Queries.GetContest;

public class GetContestQuery : IRequest<ContestInfoDto>
{
    public int Id { get; init; }
}