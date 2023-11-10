using AutoMapper;
using Common.Application.Mappings;
using VS.Domain.FC;

namespace VS.Application.Handler.Contests.DTOs;

public class ParticipantImageInfoDto : IMapFrom<ParticipantImage>
{
    public int Id { get; set; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<ParticipantImage, ParticipantImageInfoDto>()
            .ForMember(dto => dto.Id, _ => _.MapFrom(item => item.ImageId))
            ;
    }
}