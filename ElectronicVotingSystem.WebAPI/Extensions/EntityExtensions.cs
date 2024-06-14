using ElectronicVotingSystem.WebAPI.Entities;
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

    public static Election AsEntity(this UpsertElectionDto election, string userId)
    {
        return new Election
        {
            Name = election.Name,
            Description = election.Description,
            EndDate = election.EndDate,
            StartDate = election.StartDate,
            ImageUrl = election.ImageUrl,
            UserId = userId,
            Instructions = election.Instructions,
            IsElectionActive = election.IsVotingOn
        };
    }

    public static Position AsEntity(this UpsertPositionDto positionDto)
    {
        return new Position
        {
            Title = positionDto.Title,
            Description = positionDto.Description
        };
    }
}
