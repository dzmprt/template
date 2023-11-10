using AutoMapper;
using Common.Application.Mappings;
using VS.Domain.FC;

namespace VS.Application.Handler.Contests.DTOs;

public class ParticipantInfoDto : IMapFrom<Participant>
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public ParticipantImageInfoDto[] Images { get; set; } = Array.Empty<ParticipantImageInfoDto>();

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Participant, ParticipantInfoDto>()
            .ForMember(dto => dto.Id, _ => _.MapFrom(item => item.Id))
            .ForMember(dto => dto.Name, _ => _.MapFrom(item => item.Name))
            .ForMember(dto => dto.Description, _ => _.MapFrom(item => item.Description))
            .ForMember(dto => dto.Images, _ => _.MapFrom(item => item.ParticipantImages))
            ;
    }
}