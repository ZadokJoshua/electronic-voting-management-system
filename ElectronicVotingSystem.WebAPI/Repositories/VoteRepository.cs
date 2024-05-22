using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;

namespace ElectronicVotingSystem.WebAPI.Repositories
{
    public class VoteRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Vote>(dbContext), IVoteRepository
    {
    }
}
