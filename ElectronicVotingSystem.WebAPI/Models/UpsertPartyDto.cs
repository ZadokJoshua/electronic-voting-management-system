using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Models;

public class UpsertPartyDto
{
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [StringLength(100)]
    public string? Motto { get; set; }

    [StringLength(10)]
    public string? Abbreviation { get; set; }

    [StringLength(300)]
    public string? About { get; set; }
}
