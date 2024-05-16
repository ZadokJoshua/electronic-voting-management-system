using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicVotingSystem.WebAPI.Models;

public class Party
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public required Guid ElectionId { get; set; }
    public required Election Election { get; set; }
    public required string Name { get; set; }
    public string? Motto { get; set; }
    public string? About { get; set; }
    public string? IconUrl { get; set; }

    public ICollection<Candidate> Candidates { get; set; }
}
