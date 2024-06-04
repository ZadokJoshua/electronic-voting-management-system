using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class CandidateRepository(ElectronicVotingSystemDbContext dbContext) :
    GenericRepository<Candidate>(dbContext), ICandidateRepository
{
    public void DeleteCandidate(Candidate candidate)
    {
        Remove(candidate);
    }

    public async Task<Candidate> GetACandidateInPositionAsync(Guid positionId, Guid candidateId)
    {
        return await GetAll().Where(c => c.PositionId == positionId && c.Id == candidateId).FirstAsync();
    }

    public async Task<IEnumerable<Candidate>> GetAllCandidatesInElectionAsync(Guid positionId)
    {
        return await GetAll().Where(c => c.PositionId == positionId).ToListAsync();
    }
}