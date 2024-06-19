using ElectronicVotingSystem.WebAPI.Controllers;
using ElectronicVotingSystem.WebAPI.Entities;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IBallotRepository : IGenericRepository<Ballot>
{
    Task<Ballot> GetBallotByIdAsync(Guid ballotId);
    Task<IEnumerable<Ballot>> GetResultsByElectionAsync(Guid electionId);
    Task<IEnumerable<object>> GetResultsByPositionAsync(Guid positionId);
}
