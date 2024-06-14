using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entities;

namespace ElectronicVotingSystem.WebAPI.Profiles
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            // For POST request
            CreateMap<Models.UpsertCandidateDto, Candidate>();
        }
    }
}
