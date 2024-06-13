using AutoMapper;

namespace ElectronicVotingSystem.WebAPI.Profiles;

public class BallotProfile : Profile
{
    public BallotProfile()
    {
        // For POST request
        CreateMap<Models.UpsertBallotDto, Entitites.Ballot>();
    }
}