using FluentValidation;
using UserNotesSystem.Server.Models;

namespace UserNotesSystem.Server.Validations
{
    public class CreateNoteRequestValidator : AbstractValidator<CreateNoteRequest>
    {
        public CreateNoteRequestValidator() 
        { 
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
        }
    }
}
