using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class PositionRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Position>(dbContext), IPositionRepository
{
    public void DeletePostion(Position position)
    {
        _dbContext.Positions.Remove(position);
    }

    public async Task<IEnumerable<Position>> GetAllPositionsInAnElectionAsync(Guid electionId) => await _dbContext.Positions.Where(e => e.Id == electionId).ToListAsync();

    public async Task<Position> GetAPositionInAnElectionAsync(Guid electionId, Guid positionId)
    {
        return await _dbContext.Positions.Where(e => e.Id == positionId && e.ElectionId == electionId).FirstAsync();
    }
}
