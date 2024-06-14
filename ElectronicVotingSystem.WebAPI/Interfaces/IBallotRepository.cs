using ElectronicVotingSystem.WebAPI.Controllers;
using ElectronicVotingSystem.WebAPI.Entities;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IBallotRepository : IGenericRepository<Ballot>
{
    Task<Ballot> GetBallotById(Guid ballotId);
    Task<IEnumerable<object>> GetResultsByElectionAsync(Guid electionId);
    Task<IEnumerable<object>> GetResultsByPositionAsync(Guid positionId);
}
