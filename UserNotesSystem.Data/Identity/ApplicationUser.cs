using Microsoft.AspNetCore.Identity;
using UserNotesSystem.Core.Entities;

namespace UserNotesSystem.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Note> Notes { get; set; } = []; 
    }
}
