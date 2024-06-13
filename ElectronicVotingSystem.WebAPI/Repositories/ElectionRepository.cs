using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class ElectionRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Election>(dbContext), IElectionRepository
{
    public void DeleteElection(Election election)
    {
        Remove(election);
    }

    public async Task<IEnumerable<Election>> GetAllElectionsAsync(string userId)
    {
        return await GetAll().Where(e => e.UserId == userId).ToListAsync();
    }

    public async Task<Election> GetElectionByIdAsync(Guid electionId)
    {
        return await FindByCondition(e => e.Id == electionId).FirstOrDefaultAsync();
    }

    // Position
    public async Task AddPositionToAnElection(Guid electionId, Position position)
    {
        var election = await GetElectionByIdAsync(electionId);
        election?.Positions.Add(position);
    }

    // Party
    public async Task AddPartyToAnElection(Guid electionId, Party party)
    {
        var election = await GetElectionByIdAsync(electionId);
        election?.Parties.Add(party);
    }

    // Ballot
    public async Task CastBallotInAnElection(Guid electionId, Ballot ballot)
    {
        var election = await GetElectionByIdAsync(electionId);
        election?.Ballots.Add(ballot);
    }
}