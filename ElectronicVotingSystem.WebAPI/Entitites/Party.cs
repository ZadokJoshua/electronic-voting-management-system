using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicVotingSystem.WebAPI.Entitites;

public class Party : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public Guid ElectionId { get; set; }
    public Election Election { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [StringLength(100)]
    public string? Motto { get; set; }

    [StringLength(10)]
    public string? Abbreviation { get; set; }

    [StringLength(300)]
    public string? About { get; set; }

    [Url]
    public string? IconUrl { get; set; }

    public ICollection<Candidate> Candidates { get; set; } = [];
}
