using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserNotesSystem.Core.Entities;
using UserNotesSystem.Data.Identity;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace UserNotesSystem.Data.Context
{
    public class NotesContext : IdentityDbContext<ApplicationUser>
    {
        public NotesContext(DbContextOptions<NotesContext> options) : base(options) { }

        public DbSet<Note> Notes => Set<Note>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());   
        }
    }
}
