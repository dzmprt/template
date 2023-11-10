using AutoMapper;
using Common.Application.Mappings;
using MediatR;
using VS.Application.Handler.VotesSet.DTOs;

namespace VS.Application.Handler.VotesSet.Commands.AddOrUpdate;

public class AddOrUpdateVoteSetCommand : VotesSetDto, IRequest<VotesSetDto>, IMapTo<Domain.FC.VotesSet>
{
    public int ContestId { get; set; }
    
    public void CreateMap(Profile profile)
    {
        profile.CreateMap<AddOrUpdateVoteSetCommand, Domain.FC.VotesSet>()
            .ForMember(dto => dto.TicketKey, _ => _.MapFrom(item => item.TicketKey))
            .ForMember(dto => dto.Votes, _ => _.MapFrom(item => item.Votes))
            ;
    }
}