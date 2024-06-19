using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entities;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class PositionRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Position>(dbContext), IPositionRepository
{
    public async Task AddCandidateToPositionAsync(Guid electionId, Guid positionId, Candidate candidate)
    {
        var position = await GetAPositionInAnElectionAsync(electionId, positionId);
        position?.Candidates.Add(candidate);
    }
    
    public void DeletePosition(Position position)
    {
        Remove(position);
    }

    public async Task<IEnumerable<Position>> GetAllPositionsInElectionAsync(Guid electionId)
    {
        return await GetAll().Where(p => p.ElectionId == electionId).Include(p => p.Candidates).ToListAsync();
    }

    public async Task<Position> GetAPositionInAnElectionAsync(Guid electionId, Guid positionId)
    {
        return await GetAll().Where(p => p.Id == positionId && p.ElectionId == electionId).FirstOrDefaultAsync();
    }
}
