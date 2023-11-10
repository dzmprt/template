using AutoMapper;
using Common.Application.Abstractions.Persistence.Repository.Read;
using Common.Application.Abstractions.Persistence.Repository.Writing;
using Common.Application.Exceptions;
using MediatR;
using VS.Application.Handler.VotesSet.DTOs;
using VS.Domain.FC;

namespace VS.Application.Handler.VotesSet.Commands.AddOrUpdate;

public class AddOrUpdateVoteSetCommandHandler : IRequestHandler<AddOrUpdateVoteSetCommand, VotesSetDto>
{
    private readonly IBaseReadRepository<Contest> _contests;

    private readonly IBaseWriteRepository<Domain.FC.VotesSet> _voteSets;

    private readonly IBaseWriteRepository<Ticket> _tickets;

    private readonly IMapper _mapper;

    public AddOrUpdateVoteSetCommandHandler(
        IBaseReadRepository<Contest> contests,
        IBaseWriteRepository<Domain.FC.VotesSet> voteSets,
        IBaseWriteRepository<Ticket> tickets,
        IMapper mapper)
    {
        _contests = contests;
        _voteSets = voteSets;
        _tickets = tickets;
        _mapper = mapper;
    }

    public async Task<VotesSetDto> Handle(AddOrUpdateVoteSetCommand request, CancellationToken cancellationToken)
    {
        var contest = await _contests.SingleOrDefaultAsync(c => c.Id == request.ContestId, cancellationToken);

        if (contest is null)
        {
            await Task.Delay(3000, cancellationToken);
            throw new NotFoundException($"Not found contest by id {request.ContestId}", request);
        }

        var ticket = await _tickets.SingleOrDefaultAsync(t => t.Key == request.TicketKey, cancellationToken);
        if (ticket is null)
        {
            await Task.Delay(3000, cancellationToken);
            throw new NotFoundException($"Not found ticker by key {request.TicketKey}", request);
        }

        if (ticket.ContestId != contest.Id)
        {
            await Task.Delay(3000, cancellationToken);
            throw new BadOperationException($"The ticket {ticket.Key} belongs to another vote");
        }

        if (!contest.Started)
        {
            throw new BadOperationException("Voting hasn't started yet");
        }

        if (contest.Finished)
        {
            throw new BadOperationException("Voting has already ended");
        }

        var participants = contest
            .ContestCategories
            .SelectMany(c => c.Participants)
            .Where(p => request.Votes.Any(v => v.ParticipantId == p.Id))
            .ToArray();
        var participantsByCategories = participants.GroupBy(p => p.ContestCategoryId);
        var categoryWhereMoreVotesThenMaximum =
            participantsByCategories.FirstOrDefault(g => g.Count() > contest.MaximumNumberOfVotesInCategory);
        
        if (categoryWhereMoreVotesThenMaximum is not null)
        {
            throw new BadOperationException(
                $"More votes in one category than allowed. Category id:{categoryWhereMoreVotesThenMaximum.Key}");
        }

        var voteSet = await _voteSets.SingleOrDefaultAsync(v => v.TicketKey == request.TicketKey, cancellationToken);
        if (voteSet is not null)
        {
            await _voteSets.RemoveAsync(voteSet, cancellationToken);
        }

        voteSet = new Domain.FC.VotesSet
        {
            TicketKey = request.TicketKey,
            Votes = request.Votes.Where(v => participants.Any(p => p.Id == v.ParticipantId)).Select(v => new Vote(){ParticipantId = v.ParticipantId, PrizeNumber = v.PrizeNumber}).ToList(),
            ClientIp = "Obsolete",
            ClientUserAgent = "Obsolete"
        };
        await _voteSets.AddAsync(voteSet, cancellationToken);
        ticket.DateUsed = DateTime.UtcNow;
        ticket.IsUsed = true;
        await _tickets.UpdateAsync(ticket, cancellationToken);
        
        return _mapper.Map<VotesSetDto>(voteSet);
    }
}