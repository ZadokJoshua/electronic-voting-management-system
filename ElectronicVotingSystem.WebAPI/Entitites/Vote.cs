using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Entitites;

public class Vote : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(CandidateId))]
    public Guid? CandidateId { get; set; }
    public Candidate? Candidate { get; set; }

    [Required]
    [ForeignKey(nameof(BallotId))]
    public Guid? BallotId { get; set; }
    public Ballot? Ballot { get; set; }
}
