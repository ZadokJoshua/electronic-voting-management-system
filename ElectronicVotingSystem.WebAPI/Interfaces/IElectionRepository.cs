﻿using ElectronicVotingSystem.WebAPI.Entitites;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IElectionRepository : IGenericRepository<Election>
{
    Task<IEnumerable<Election>> GetAllElectionsAsync(string userId);
    Task<Election> GetElectionByIdAsync(Guid electionId);
    void DeleteElection(Election election);
}