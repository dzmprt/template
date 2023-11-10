using Common.Application.Mappings;

namespace VS.Application.Handler.VotesSet.DTOs;

public class VotesSetDto : IMapFrom<Domain.FC.VotesSet>, IMapTo<Domain.FC.VotesSet>
{
    public string TicketKey { get; set; } = default!;
    
    public int ContestId { get; set; }

    public VoteDto[] Votes { get; set; } = Array.Empty<VoteDto>();

}