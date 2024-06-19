using ElectronicVotingSystem.WebAPI.Controllers;
using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entities;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class BallotRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Ballot>(dbContext), IBallotRepository
{
    public async Task<Ballot> GetBallotByIdAsync(Guid ballotId)
    {
        return await GetAll().Where(b => b.Id == ballotId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Ballot>> GetResultsByElectionAsync(Guid electionId)
    {
        return await GetAll().Where(b => b.ElectionId == electionId).Include(b => b.PositionCandidates).ToListAsync();
    }

    public Task<IEnumerable<object>> GetResultsByPositionAsync(Guid positionId)
    {
        throw new NotImplementedException();
    }
}
