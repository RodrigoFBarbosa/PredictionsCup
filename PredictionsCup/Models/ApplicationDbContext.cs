using Microsoft.EntityFrameworkCore;
using PredictionsCup.Models;

namespace PredictionsCup.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Prediction> Predictions { get; set; }
    public DbSet<Finalist> Finalists { get; set; }
    public DbSet<SemiFinalist> Semifinalists { get; set; }
    public DbSet<TopScorer> TopScorers { get; set; }
    public DbSet<BestPlayer> BestPlayers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.User)
            .WithMany(u => u.Predictions)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Finalist>()
            .HasOne(f => f.User)
            .WithMany(u => u.Finalists)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SemiFinalist>()
            .HasOne(s => s.User)
            .WithMany(u => u.Semifinalists)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasOne(u => u.TopScorer)
            .WithOne(ts => ts.User)
            .HasForeignKey<TopScorer>(ts => ts.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasOne(u => u.BestPlayer)
            .WithOne(bp => bp.User)
            .HasForeignKey<BestPlayer>(bp => bp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}