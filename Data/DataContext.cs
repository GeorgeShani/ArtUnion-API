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
    public DbSet<ArtworkLike> ArtworkLikes { get; set; }
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
            .OnDelete(DeleteBehavior.Restrict);
        
        // Artwork -> Category
        modelBuilder.Entity<Artwork>()
            .HasOne(a => a.Category)
            .WithMany(c => c.Artworks)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // ArtworkLike (Many-to-Many: User <-> Artwork)
        modelBuilder.Entity<ArtworkLike>()
            .HasKey(al => new { al.UserId, al.ArtworkId });

        modelBuilder.Entity<ArtworkLike>()
            .HasOne(al => al.User)
            .WithMany(u => u.LikedArtworks)
            .HasForeignKey(al => al.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ArtworkLike>()
            .HasOne(al => al.Artwork)
            .WithMany(a => a.Likes)
            .HasForeignKey(al => al.ArtworkId)
            .OnDelete(DeleteBehavior.Restrict);

        // Critique -> Artwork
        modelBuilder.Entity<Critique>()
            .HasOne(c => c.Artwork)
            .WithMany(a => a.Critiques)
            .HasForeignKey(c => c.ArtworkId)
            .OnDelete(DeleteBehavior.Restrict);

        // Critique -> User (Critic)
        modelBuilder.Entity<Critique>()
            .HasOne(c => c.Critic)
            .WithMany(u => u.Critiques)
            .HasForeignKey(c => c.CriticId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Subscription -> Subscriber (User)
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Subscriber)
            .WithMany(u => u.Following)
            .HasForeignKey(s => s.SubscriberId)
            .OnDelete(DeleteBehavior.Restrict);

        // Subscription -> Artist (User)
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Artist)
            .WithMany(u => u.Followers)
            .HasForeignKey(s => s.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}