using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entities;
using ElectronicVotingSystem.WebAPI.Models;

namespace ElectronicVotingSystem.WebAPI.Profiles;

public class PositionCandidateProfile : Profile
{
    public PositionCandidateProfile()
    {
        // POST
        CreateMap<Models.UpsertPositionCandidate, PositionCandidate>();
    }
}
