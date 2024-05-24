using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Entitites;

public class Result : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(ElectionId))]
    public required Guid ElectionId { get; set; }
    public Election Election { get; set; }

    [Required]
    [ForeignKey(nameof(PositionId))]
    public required Guid PositionId { get; set; }
    public Position Position { get; set; }

    [Required]
    [ForeignKey(nameof(CandidateId))]
    public required Guid CandidateId { get; set; }
    public Candidate Candidate { get; set; }

    public int VoteCount { get; set; }
}
