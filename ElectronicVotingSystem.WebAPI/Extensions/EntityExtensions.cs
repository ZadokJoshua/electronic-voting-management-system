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
            HasVotingEnded = election.HasVotingEnded,
            ImageUrl = election.ImageUrl,
            Instructions = election.Instructions,
            IsVotingOn = election.IsVotingOn
        };
    }
}
