﻿namespace ArtUnion_API.Models;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public ICollection<Artwork>? Artworks { get; set; }
}