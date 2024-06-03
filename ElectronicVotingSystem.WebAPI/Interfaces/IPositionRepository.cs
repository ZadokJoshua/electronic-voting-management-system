using ElectronicVotingSystem.WebAPI.Entitites;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IPositionRepository : IGenericRepository<Position>
{
    Task<IEnumerable<Position>> GetAllPositionsInAnElectionAsync(Guid electionId);
    Task<Position> GetAPositionInAnElectionAsync(Guid electionId, Guid positionId);
    void DeletePosition(Position position);
}
