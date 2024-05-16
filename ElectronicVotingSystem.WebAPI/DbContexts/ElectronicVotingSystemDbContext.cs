using ElectronicVotingSystem.WebAPI.Entities;
using ElectronicVotingSystem.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.DbContexts;

public class ElectronicVotingSystemDbContext(DbContextOptions<ElectronicVotingSystemDbContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Election> Elections {  get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Vote> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Party>()
            .HasOne(p => p.Election)
            .WithMany(e => e.Parties)
            .HasForeignKey(p => p.ElectionId);

        modelBuilder.Entity<Position>()
           .HasOne(p => p.Election)
           .WithMany(e => e.Positions)
           .HasForeignKey(p => p.ElectionId);

        modelBuilder.Entity<Candidate>()
           .HasOne(c => c.Party)
           .WithMany(p => p.Candidates)
           .HasForeignKey(c => c.PartyId);

        modelBuilder.Entity<Candidate>()
           .HasOne(c => c.Position)
           .WithMany(p => p.Candidates)
           .HasForeignKey(p => p.PositionId);
    }
}
