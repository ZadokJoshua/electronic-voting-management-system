using ElectronicVotingSystem.WebAPI.Entities;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IPositionRepository : IGenericRepository<Position>
{
    Task<IEnumerable<Position>> GetAllPositionsInElectionAsync(Guid electionId);
    Task<Position> GetAPositionInAnElectionAsync(Guid electionId, Guid positionId);
    void DeletePosition(Position position);
    Task AddCandidateToPositionAsync(Guid electionId, Guid positionId, Candidate candidate);
}
