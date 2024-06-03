using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Models;

public class UpsertPositionDto
{
    [Required]
    [StringLength(100)]
    public required string Title { get; set; }

    [StringLength(300)]
    public string? Description { get; set; }
}
