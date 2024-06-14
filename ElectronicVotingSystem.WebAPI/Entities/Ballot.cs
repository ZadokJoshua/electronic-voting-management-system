using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Entities;

public class Ballot : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(ElectionId))]
    public Guid? ElectionId { get; set; }
    public Election? Election { get; set; }

    [Required]
    [ForeignKey(nameof(VoterId))]
    public string? VoterId { get; set; }

    public AppUser? User { get; set; }

    public ICollection<PositionCandidate> PositionCandidates { get; set; } = [];

    public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;
}
