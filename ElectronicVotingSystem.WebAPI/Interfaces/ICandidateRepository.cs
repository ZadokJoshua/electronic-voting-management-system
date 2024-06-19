using ElectronicVotingSystem.WebAPI.Entities;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface ICandidateRepository : IGenericRepository<Candidate>
{
    Task<IEnumerable<Candidate>> GetAllCandidatesContestingForPositionAsync(Guid positionId);
    Task<Candidate> GetACandidateInPositionAsync(Guid positionId, Guid candidateId);
    void DeleteCandidate(Candidate candidate);
}