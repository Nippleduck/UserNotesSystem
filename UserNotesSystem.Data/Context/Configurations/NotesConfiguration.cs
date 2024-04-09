using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserNotesSystem.Core.Entities;
using UserNotesSystem.Data.Identity;

namespace UserNotesSystem.Data.Context.Configurations
{
    internal class NotesConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder
                .HasOne<ApplicationUser>()
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(n => n.Title)
                .HasMaxLength(50);

            builder
                .Property(n => n.Description)
                .HasMaxLength(200);
        }
    }
}
