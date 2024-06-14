using ElectronicVotingSystem.WebAPI.Entities;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IPartyRepository : IGenericRepository<Party>
{
    void DeleteParty(Party party);
    Task<IEnumerable<Party>> GetAllPartiesInElectionAsync(Guid electionId);
    Task<Party> GetPartyInElectionAsync(Guid electionId, Guid partyId);
}
