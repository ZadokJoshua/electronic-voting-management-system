using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entities;
namespace ElectronicVotingSystem.WebAPI.Profiles;

public class ElectionProfile : Profile
{
    public ElectionProfile()
    {
        // For POST request
        CreateMap<Models.UpsertElectionDto, Election>();
    }
}
