using ElectronicVotingSystem.WebAPI.Controllers;
using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class BallotRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Ballot>(dbContext), IBallotRepository
{
    // GetAllCastedBallotsInElection
    public Task<Ballot> GetBallotById(Guid electionId, Guid ballotId)
    {
        throw new NotImplementedException();
    }


    Task<IEnumerable<object>> IBallotRepository.GetResultsByElectionAsync(Guid electionId)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<object>> IBallotRepository.GetResultsByPositionAsync(Guid positionId)
    {
        throw new NotImplementedException();
    }
}
