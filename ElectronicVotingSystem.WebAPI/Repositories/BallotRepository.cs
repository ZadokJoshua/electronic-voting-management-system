using ElectronicVotingSystem.WebAPI.Controllers;
using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entities;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class BallotRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Ballot>(dbContext), IBallotRepository
{
    // GetAllCastedBallotsInElection
    public async Task<Ballot> GetBallotById(Guid ballotId)
    {
        return await GetAll().Where(b => b.Id == ballotId).FirstOrDefaultAsync();
    }

    public Task<IEnumerable<object>> GetResultsByElectionAsync(Guid electionId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<object>> GetResultsByPositionAsync(Guid positionId)
    {
        throw new NotImplementedException();
    }
}
