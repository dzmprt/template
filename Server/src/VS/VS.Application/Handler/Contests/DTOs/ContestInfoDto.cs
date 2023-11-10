using AutoMapper;
using Common.Application.Mappings;
using VS.Domain.FC;

namespace VS.Application.Handler.Contests.DTOs;

public class ContestInfoDto : IMapFrom<Contest>
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public bool Started { get; set; }

    public bool Finished { get; set; }

    public int MaximumNumberOfVotesInCategory { get; set; }

    public List<ContestCategoryInfoDto> ContestCategories { get; set; } = new List<ContestCategoryInfoDto>();

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Contest, ContestInfoDto>()
            .ForMember(dto => dto.Id, _ => _.MapFrom(item => item.Id))
            .ForMember(dto => dto.Name, _ => _.MapFrom(item => item.Name))
            .ForMember(dto => dto.Started, _ => _.MapFrom(item => item.Started))
            .ForMember(dto => dto.Finished, _ => _.MapFrom(item => item.Finished))
            .ForMember(dto => dto.MaximumNumberOfVotesInCategory,
                _ => _.MapFrom(item => item.MaximumNumberOfVotesInCategory))
            .ForMember(dto => dto.ContestCategories, _ => _.MapFrom(item => item.ContestCategories))
            ;
    }
}