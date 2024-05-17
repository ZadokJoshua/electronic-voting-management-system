using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Entitites;

public class Vote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(ElectionId))]
    public Guid ElectionId { get; set; }
    public Election Election { get; set; }

    public bool HasVoted { get; set; }
    public DateTime VoteDate { get; set; }
}
