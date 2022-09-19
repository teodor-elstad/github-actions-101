namespace Notes.Api.Database;

using Microsoft.EntityFrameworkCore;
using Notes.Api.Models;

public class NotesDb : DbContext
{
    public NotesDb() { }

    public NotesDb(DbContextOptions<NotesDb> options) : base(options) { }

    public DbSet<Note> Notes { get; set; }

    public void SeedData()
    {
        this.Database.EnsureDeleted();
        this.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Note>()
            .HasData(new Note[]
            {
                new Note { Id = 1, Content = "Husk å kjøpe brød" },
                new Note { Id = 2, Content = "Hva var passordet til databasen igjen?" }
            });
    }
}