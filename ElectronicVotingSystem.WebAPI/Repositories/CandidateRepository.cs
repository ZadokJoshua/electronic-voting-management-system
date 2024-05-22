using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class CandidateRepository(ElectronicVotingSystemDbContext dbContext) :
    GenericRepository<Candidate>(dbContext), ICandidateRepository
{
}