using AutoMapper;
using Common.Application.Abstractions.Persistence.Repository.Read;
using Common.Application.Exceptions;
using MediatR;
using VS.Application.Handler.VotesSet.DTOs;
using VS.Domain.FC;

namespace VS.Application.Handler.VotesSet.Queries.GetVotesSet;

public class VotesSetQueryHandler : IRequestHandler<VotesSetQuery, VotesSetDto>
{
    private readonly IBaseReadRepository<Domain.FC.VotesSet> _voteSets;
    private readonly IBaseReadRepository<Ticket> _tickets;
    private readonly IMapper _mapper;

    public VotesSetQueryHandler(IBaseReadRepository<Domain.FC.VotesSet> voteSets, IBaseReadRepository<Ticket> tickets,
        IMapper mapper)
    {
        _voteSets = voteSets;
        _tickets = tickets;
        _mapper = mapper;
    }

    public async Task<VotesSetDto> Handle(VotesSetQuery request, CancellationToken cancellationToken)
    {
        var ticket =
            await _tickets.SingleOrDefaultAsync(t => t.Key == request.Ticket && t.ContestId == request.ContestId,
                cancellationToken);
        
        if (ticket is null)
        {
            await Task.Delay(3000, cancellationToken);
            throw new NotFoundException(request);
        }

        var voteSet = await _voteSets.SingleOrDefaultAsync(v => v.TicketKey == request.Ticket,
            cancellationToken);

        if (voteSet is null)
        {
            voteSet = new Domain.FC.VotesSet()
            {
                TicketKey = request.Ticket,
            };
        }
        
        var dto = _mapper.Map<VotesSetDto>(voteSet);
        dto.ContestId = request.ContestId;

        return dto;
    }
}