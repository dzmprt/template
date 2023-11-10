using MediatR;
using VS.Application.Handler.VotesSet.DTOs;

namespace VS.Application.Handler.VotesSet.Queries.GetVotesSet;

public class VotesSetQuery: IRequest<VotesSetDto>
{
    public string Ticket { get; init; } = default!;
    
    public int ContestId { get; set; }
}