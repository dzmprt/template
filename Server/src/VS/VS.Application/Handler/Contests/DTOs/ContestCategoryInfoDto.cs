using AutoMapper;
using Common.Application.Mappings;
using VS.Domain.FC;

namespace VS.Application.Handler.Contests.DTOs;

public class ContestCategoryInfoDto : IMapFrom<ContestCategory>
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public ParticipantInfoDto[] Participants { get; set; } = Array.Empty<ParticipantInfoDto>();
    
    public void CreateMap(Profile profile)
    {
        profile.CreateMap<ContestCategory, ContestCategoryInfoDto>()
            .ForMember(dto => dto.Id, _ => _.MapFrom(item => item.Id))
            .ForMember(dto => dto.Name, _ => _.MapFrom(item => item.Name))
            .ForMember(dto => dto.Participants, _ => _.MapFrom(item => item.Participants))
            ;
    }
}