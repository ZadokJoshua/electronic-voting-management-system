using AutoMapper;

namespace ElectronicVotingSystem.WebAPI.Profiles;

public class PartyProfile : Profile
{
    public PartyProfile()
    {
        // For POST request
        CreateMap<Models.UpsertPartyDto, Entitites.Party>();
    }
}
