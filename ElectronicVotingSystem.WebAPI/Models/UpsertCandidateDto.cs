using ElectronicVotingSystem.WebAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Models;

public class UpsertCandidateDto
{
    [Required]
    public string UserId { get; set; }
    public Guid? PartyId { get; set; }
}
