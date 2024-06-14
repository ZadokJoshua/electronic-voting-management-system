using ElectronicVotingSystem.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Models;

public class UpsertPositionCandidate
{
    [Required]
    public Guid? PositionId { get; set; }

    [Required]
    public Guid? CandidateId { get; set; }
}
