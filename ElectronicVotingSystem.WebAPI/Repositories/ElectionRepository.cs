using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class ElectionRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Election>(dbContext), IElectionRepository
{
    public void DeleteElection(Election election)
    {
        _dbContext.Elections.Remove(election);
    }

    public async Task<IEnumerable<Election>> GetAllElectionsAsync(string userId)
    {
        return await GetAll().Where(e => e.UserId == userId).ToListAsync();
    }

    public async Task<Election> GetElectionByIdAsync(Guid electionId)
    {
        return await FindByCondition(e => e.Id == electionId).FirstOrDefaultAsync();
    }
}