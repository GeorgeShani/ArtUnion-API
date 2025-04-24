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
}