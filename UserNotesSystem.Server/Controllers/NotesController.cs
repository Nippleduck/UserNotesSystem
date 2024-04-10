using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserNotesSystem.Core.Entities;
using UserNotesSystem.Data.Context;
using UserNotesSystem.Server.Models;

namespace UserNotesSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController(NotesContext context, CurrentUserAccessor userAccessor) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Note[]>> Get()
        {
            var notes = await context.Notes
                .Where(n => n.OwnerId == userAccessor.UserId)
                .ToArrayAsync();

            return Ok(notes);
        }   

        [HttpPost] 
        public async Task<ActionResult<string>> Add([FromBody] CreateNoteRequest request)
        {
            var note = new Note
            {
                Title = request.Title,
                Description = request.Description,
                CreationDate = DateTime.UtcNow,
                OwnerId = userAccessor.UserId,
            };

            await context.Notes.AddAsync(note);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), note.Id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var note = await context.Notes.FindAsync(id);
            if (note is null) return NotFound( 
                new{ message = $"Note with id:{id} not found" } );

            if (note.OwnerId != userAccessor.UserId) return BadRequest(
                new { message = "Attempted to remove note which belongs to different user" });

            context.Notes.Remove(note);
            await context.SaveChangesAsync(true);

            return NoContent();
        }
    }
}
