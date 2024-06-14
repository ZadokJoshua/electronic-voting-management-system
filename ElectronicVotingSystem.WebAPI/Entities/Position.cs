using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Entities;

public class Position : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    //[Required]
    [ForeignKey(nameof(ElectionId))]
    public Guid ElectionId { get; set; }
    public Election Election { get; set; }

    [Required]
    [StringLength(100)]
    public required string Title { get; set; }

    [StringLength(300)]
    public string? Description { get; set; }

    public ICollection<Candidate> Candidates { get; set; } = [];
}
