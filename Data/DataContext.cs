using ArtUnion_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtUnion_API.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Artwork> Artworks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Critique> Critiques { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ArtUnion");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Portfolio -> User (Artist)
        modelBuilder.Entity<Portfolio>()
            .HasOne(p => p.Artist)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.ArtistId)
            .OnDelete(DeleteBehavior.Cascade); // If Artist is deleted, delete Portfolios

        // Artwork -> Portfolio
        modelBuilder.Entity<Artwork>()
            .HasOne(a => a.Portfolio)
            .WithMany(p => p.Artworks)
            .HasForeignKey(a => a.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade); // If Portfolio is deleted, delete Artworks

        // Artwork -> Category
        modelBuilder.Entity<Artwork>()
            .HasOne(a => a.Category)
            .WithMany(c => c.Artworks)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // If Category is deleted, don't delete artworks automatically

        // Critique -> Artwork
        modelBuilder.Entity<Critique>()
            .HasOne(c => c.Artwork)
            .WithMany(a => a.Critiques)
            .HasForeignKey(c => c.ArtworkId)
            .OnDelete(DeleteBehavior.Cascade); // If Artwork is deleted, delete Critiques

        // Critique -> User (Critic)
        modelBuilder.Entity<Critique>()
            .HasOne(c => c.Critic)
            .WithMany(u => u.Critiques)
            .HasForeignKey(c => c.CriticId)
            .OnDelete(DeleteBehavior.Restrict); // If Critic user is deleted, critiques stay

        // Subscription -> Subscriber (User)
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Subscriber)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(s => s.SubscriberId)
            .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete Subscriber

        // Subscription -> Artist (User)
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Artist)
            .WithMany(u => u.Followers)
            .HasForeignKey(s => s.ArtistId)
            .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete Artist
    }
}