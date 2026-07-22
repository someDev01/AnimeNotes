using AnimeNotes.Models.Note;
using AnimeNotes.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeNotes.Db.Configurations;

public class AnimeNoteConfiguration : IEntityTypeConfiguration<AnimeNote>
{
    public void Configure(EntityTypeBuilder<AnimeNote> builder)
    {
        builder.ToTable("Notes");
        builder.HasKey(an=> an.Id);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
