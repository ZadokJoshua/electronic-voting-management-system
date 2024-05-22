using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class PositionRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Position>(dbContext), IPositionRepository
{
}
