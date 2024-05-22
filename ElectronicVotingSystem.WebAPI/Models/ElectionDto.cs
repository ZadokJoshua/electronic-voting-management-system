using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ElectronicVotingSystem.WebAPI.Models
{
    public record ElectionDto
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(300)]
        public required string Description { get; set; }

        [StringLength(100)]
        public string? Instructions { get; set; }

        [Url]
        public string? ImageUrl { get; set; }
        public string? ElectionAccessKey { get; set; }
        public bool IsVotingOn { get; set; }
        public bool HasVotingEnded { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? TimeZone { get; set; }
    }
}