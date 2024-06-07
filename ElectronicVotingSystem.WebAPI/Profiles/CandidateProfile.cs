using AutoMapper;

namespace ElectronicVotingSystem.WebAPI.Profiles
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            // For POST request
            CreateMap<Models.UpsertCandidateDto, Entitites.Candidate>();
        }
    }
}
