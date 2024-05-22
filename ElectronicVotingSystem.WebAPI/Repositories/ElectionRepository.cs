using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class ElectionRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Election>(dbContext), IElectionRepository
{
}