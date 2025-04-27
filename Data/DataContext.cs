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
        // User (Artist) -> Artwork
        modelBuilder.Entity<Artwork>()
            .HasOne(a => a.Artist)
            .WithMany(u => u.Artworks)
            .HasForeignKey(a => a.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Portfolio -> User (Artist)
        modelBuilder.Entity<Portfolio>()
            .HasOne(p => p.Artist)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        // Artwork -> Portfolio
        modelBuilder.Entity<Artwork>()
            .HasOne(a => a.Portfolio)
            .WithMany(p => p.Artworks)
            .HasForeignKey(a => a.PortfolioId)
            .IsRequired(false) 
            .OnDelete(DeleteBehavior.Cascade);
        
        // Artwork -> Category
        modelBuilder.Entity<Artwork>()
            .HasOne(a => a.Category)
            .WithMany(c => c.Artworks)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // If Category is deleted, don't delete artworks

        // Critique -> Artwork
        modelBuilder.Entity<Critique>()
            .HasOne(c => c.Artwork)
            .WithMany(a => a.Critiques)
            .HasForeignKey(c => c.ArtworkId)
            .OnDelete(DeleteBehavior.Restrict); // When critique is deleted, no effect on artwork

        // Critique -> User (Critic)
        modelBuilder.Entity<Critique>()
            .HasOne(c => c.Critic)
            .WithMany(u => u.Critiques)
            .HasForeignKey(c => c.CriticId)
            .OnDelete(DeleteBehavior.Cascade); // When critic is deleted, delete all their critiques
        
        // Subscription -> Subscriber (User)
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Subscriber)
            .WithMany(u => u.Following) // Renamed from Subscriptions
            .HasForeignKey(s => s.SubscriberId)
            .OnDelete(DeleteBehavior.Cascade); // Delete subscriptions when subscriber is deleted

        // Subscription -> Artist (User)
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Artist)
            .WithMany(u => u.Followers)
            .HasForeignKey(s => s.ArtistId)
            .OnDelete(DeleteBehavior.Restrict); // When subscription is deleted, no effect on artist
    }
}