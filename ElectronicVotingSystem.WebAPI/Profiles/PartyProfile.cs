using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entities;

namespace ElectronicVotingSystem.WebAPI.Profiles;

public class PartyProfile : Profile
{
    public PartyProfile()
    {
        // For POST request
        CreateMap<Models.UpsertPartyDto, Party>();
    }
}
