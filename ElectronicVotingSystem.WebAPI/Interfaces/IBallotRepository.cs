using ElectronicVotingSystem.WebAPI.Controllers;
using ElectronicVotingSystem.WebAPI.Entitites;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IBallotRepository : IGenericRepository<Ballot>
{
    Task<Ballot> GetBallotById(Guid electionId, Guid ballotId);
    Task<IEnumerable<object>> GetResultsByElectionAsync(Guid electionId);
    Task<IEnumerable<object>> GetResultsByPositionAsync(Guid positionId);
}
