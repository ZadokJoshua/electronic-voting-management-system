using ElectronicVotingSystem.WebAPI.Entitites;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface ICandidateRepository : IGenericRepository<Candidate>
{
    Task<IEnumerable<Candidate>> GetAllCandidatesInElectionAsync(Guid positionId);
    Task<Candidate> GetACandidateInPositionAsync(Guid positionId, Guid candidateId);
    void DeleteCandidate(Candidate candidate);
}