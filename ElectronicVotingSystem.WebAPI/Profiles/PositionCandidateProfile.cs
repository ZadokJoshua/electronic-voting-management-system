using AutoMapper;

namespace ElectronicVotingSystem.WebAPI.Profiles;

public class PositionCandidateProfile : Profile
{
    public PositionCandidateProfile()
    {
        // POST
        CreateMap<Models.UpsertPositionCandidate, Entitites.PositionCandidate>();
    }
}
