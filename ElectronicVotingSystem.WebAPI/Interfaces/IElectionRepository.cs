using ElectronicVotingSystem.WebAPI.Entities;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IElectionRepository : IGenericRepository<Election>
{
    Task<IEnumerable<Election>> GetAllElectionsAsync(string userId);
    Task<Election> GetElectionByIdAsync(Guid electionId);
    void DeleteElection(Election election);
    Task AddPositionToAnElectionAsync(Guid electionId, Position position);
    Task AddPartyToAnElectionAsync(Guid electionId, Party party);
    Task CastBallotInAnElectionAsync(Guid electionId, Ballot ballot);
}