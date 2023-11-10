using AutoMapper;
using Common.Application.Mappings;
using VS.Domain.FC;

namespace VS.Application.Handler.VotesSet.DTOs;

public class VoteDto : IMapFrom<Vote>, IMapTo<Vote>
{
    public int ParticipantId { get; set; }
    
    public int CategoryId { get; set; }

    public int PrizeNumber { get; set; }
    
    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Vote, VoteDto>()
            .ForMember(dto => dto.ParticipantId, _ => _.MapFrom(item => item.ParticipantId))
            .ForMember(dto => dto.CategoryId, _ => _.MapFrom(item => item.Participant.ContestCategoryId))
            .ForMember(dto => dto.PrizeNumber, _ => _.MapFrom(item => item.PrizeNumber))
            ;
    }
}