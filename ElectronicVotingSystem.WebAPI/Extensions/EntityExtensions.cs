using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Models;

namespace ElectronicVotingSystem.WebAPI.Extensions;

public static class EntityExtensions
{
    public static ElectionDto AsDto(this Election election)
    {
        return new ElectionDto
        {
            Name = election.Name,
            Description = election.Description,
            EndDate = election.EndDate,
            StartDate = election.StartDate,
            ImageUrl = election.ImageUrl,
            Instructions = election.Instructions,
            IsVotingOn = election.IsElectionActive
        };
    }

    public static Election AsEntity(this CreateElectionDto election)
    {
        return new Election
        {
            Name = election.Name,
            Description = election.Description,
            EndDate = election.EndDate,
            StartDate = election.StartDate,
            ImageUrl = election.ImageUrl,
            Instructions = election.Instructions,
            IsElectionActive = election.IsVotingOn
        };
    }
}
