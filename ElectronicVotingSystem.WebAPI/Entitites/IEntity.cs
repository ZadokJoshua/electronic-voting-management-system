using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicVotingSystem.WebAPI.Entitites;

public interface IEntity
{
    public Guid Id { get; set; }
    // public DateTime CreatedAt { get; set; }
}