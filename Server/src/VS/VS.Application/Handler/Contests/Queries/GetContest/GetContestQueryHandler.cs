using AutoMapper;
using Common.Application.Abstractions.Persistence.Repository.Read;
using Common.Application.Exceptions;
using MediatR;
using VS.Application.Handler.Contests.DTOs;
using VS.Application.Utils;
using VS.Domain.FC;

namespace VS.Application.Handler.Contests.Queries.GetContest;

public class GetContestQueryHandler : IRequestHandler<GetContestQuery, ContestInfoDto>
{
    private readonly IBaseReadRepository<Contest> _contests;
    
    private readonly IMapper _mapper;

    public GetContestQueryHandler(IBaseReadRepository<Contest> contests, IMapper mapper)
    {
        _contests = contests;
        _mapper = mapper;
    }
    public async Task<ContestInfoDto> Handle(GetContestQuery request, CancellationToken cancellationToken)
    {
        var contest = await _contests.SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (contest == null)
        {
            throw new NotFoundException(request);
        }
        
        foreach (var category in contest.ContestCategories)
        {
            category.Participants = category.Participants.OrderBy(p => p.Name, new CustomComparer()).ToList();
        }

        contest.ContestCategories = contest.ContestCategories.OrderBy(c => c.Name, new CustomComparer()).ToList();

        return _mapper.Map<ContestInfoDto>(contest);
    }
}