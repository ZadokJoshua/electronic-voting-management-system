using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class PartyRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Party>(dbContext), IPartyRepository
{
}
