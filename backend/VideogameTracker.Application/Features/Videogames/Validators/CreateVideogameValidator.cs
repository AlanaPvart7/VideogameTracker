using FluentValidation;
using VideogameTracker.Application.Features.Videogames.DTOs;

namespace VideogameTracker.Application.Features.Videogames.Validators;

public class CreateVideogameValidator : AbstractValidator<CreateVideogameRequestDto>
{
    public CreateVideogameValidator() 
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del videojuego no puede estar vacío.")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El nombre es demasiado largo.");
    }
}