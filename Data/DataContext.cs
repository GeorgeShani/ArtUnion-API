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
        // User (Artist) -> Artwork: Cascade (when User is deleted, delete their artworks)
        modelBuilder.Entity<Artwork>()
            .HasOne(a => a.Artist)
            .WithMany(u => u.Artworks)
            .HasForeignKey(a => a.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Portfolio -> User (Artist): Cascade (when User is deleted, delete their portfolios)
        modelBuilder.Entity<Portfolio>()
            .HasOne(p => p.Artist)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Artwork -> Portfolio: Restrict (prevent deleting Portfolio if it has Artworks)
        modelBuilder.Entity<Artwork>()
            .HasOne(a => a.Portfolio)
            .WithMany(p => p.Artworks)
            .HasForeignKey(a => a.PortfolioId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Artwork -> Category: Restrict (prevent deleting Category if it has Artworks)
        modelBuilder.Entity<Artwork>()
            .HasOne(a => a.Category)
            .WithMany(c => c.Artworks)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // ArtworkLike (Many-to-Many: User <-> Artwork)
        modelBuilder.Entity<ArtworkLike>()
            .HasKey(al => new { al.UserId, al.ArtworkId });
        
        // ArtworkLike -> User: Cascade (when User is deleted, delete their likes)
        modelBuilder.Entity<ArtworkLike>()
            .HasOne(al => al.User)
            .WithMany(u => u.LikedArtworks)
            .HasForeignKey(al => al.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // ArtworkLike -> Artwork: Restrict (prevent deleting Artwork if it has Likes)
        modelBuilder.Entity<ArtworkLike>()
            .HasOne(al => al.Artwork)
            .WithMany(a => a.Likes)
            .HasForeignKey(al => al.ArtworkId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Critique -> Artwork: Restrict (prevent deleting Artwork if it has Critiques)
        modelBuilder.Entity<Critique>()
            .HasOne(c => c.Artwork)
            .WithMany(a => a.Critiques)
            .HasForeignKey(c => c.ArtworkId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Critique -> User (Critic): Cascade (when User is deleted, delete their critiques)
        modelBuilder.Entity<Critique>()
            .HasOne(c => c.Critic)
            .WithMany(u => u.Critiques)
            .HasForeignKey(c => c.CriticId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Subscription -> Subscriber (User): ClientSetNull (prevents multiple cascade paths error)
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Subscriber)
            .WithMany(u => u.Following)
            .HasForeignKey(s => s.SubscriberId)
            .OnDelete(DeleteBehavior.ClientSetNull); // Uses NO ACTION in database but EF Core handles deletion
        
        // Subscription -> Artist (User): Cascade (when User is deleted, delete subscriptions to them)
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Artist)
            .WithMany(u => u.Followers)
            .HasForeignKey(s => s.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}