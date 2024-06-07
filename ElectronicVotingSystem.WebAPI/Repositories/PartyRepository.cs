using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class PartyRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Party>(dbContext), IPartyRepository
{
    public void DeleteParty(Party party)
    {
        Remove(party);
    }

    public async Task<IEnumerable<Party>> GetAllPartiesInElectionAsync(Guid electionId)
    {
        return await GetAll().Where(p => p.ElectionId == electionId).ToListAsync();
    }

    public async Task<Party> GetPartyInElectionAsync(Guid electionId, Guid partyId)
    {
        return await GetAll().Where(p => p.Id == partyId && p.ElectionId == electionId).FirstOrDefaultAsync();
    }
}
