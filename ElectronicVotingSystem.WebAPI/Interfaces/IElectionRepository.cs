using ElectronicVotingSystem.WebAPI.Entitites;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IElectionRepository : IGenericRepository<Election>
{
    Task<IEnumerable<Election>> GetAllElectionsAsync();
    Task<Election> GetElectionByIdAsync(Guid electionId);
    void DeleteElection(Election election);
}