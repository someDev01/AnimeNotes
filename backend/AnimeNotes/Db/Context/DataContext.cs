using AnimeNotes.Models.Note;
using AnimeNotes.Models.Session;
using AnimeNotes.Models.User;
using Microsoft.EntityFrameworkCore;

namespace AnimeNotes.Db.Context;

public class DataContext(DbContextOptions<DataContext> options): DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Session> Sessions { get; set; }

    public DbSet<AnimeNote> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
