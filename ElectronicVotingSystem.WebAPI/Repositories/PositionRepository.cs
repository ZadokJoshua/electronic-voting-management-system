﻿using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class PositionRepository(ElectronicVotingSystemDbContext dbContext) : GenericRepository<Position>(dbContext), IPositionRepository
{
    public async Task AddCandidateToPosition(Guid electionId, Guid positionId, Candidate candidate)
    {
        var position = await GetAPositionInAnElectionAsync(electionId, positionId);
        position?.Candidates.Add(candidate);
    }
    

    public void DeletePosition(Position position)
    {
        Remove(position);
    }

    public async Task<IEnumerable<Position>> GetAllPositionsInAnElectionAsync(Guid electionId)
    {
        return await GetAll().Where(p => p.ElectionId == electionId).ToListAsync();
    }

    public async Task<Position> GetAPositionInAnElectionAsync(Guid electionId, Guid positionId)
    {
        return await GetAll().Where(p => p.Id == positionId && p.ElectionId == electionId).FirstOrDefaultAsync();
    }
}
