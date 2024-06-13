using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Models;

namespace ElectronicVotingSystem.WebAPI.Profiles;

public class BallotProfile : Profile
{
    public BallotProfile()
    {
        // Map Nested Collections
        CreateMap<UpsertBallotDto, Ballot>()
            .ForMember(
            dest => dest.PositionCandidates,
            opt => opt.MapFrom(src => src.UpsertPositionCandidates));
    }
}