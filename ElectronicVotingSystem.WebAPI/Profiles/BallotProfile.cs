using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entities;
using ElectronicVotingSystem.WebAPI.Models;

namespace ElectronicVotingSystem.WebAPI.Profiles;

public class BallotProfile : Profile
{
    public BallotProfile()
    {
        // Map Nested Collections
        CreateMap<Models.UpsertBallotDto, Entities.Ballot>()
            .ForMember(
            dest => dest.PositionCandidates,
            opt => opt.MapFrom(src => src.UpsertPositionCandidates));
    }
}