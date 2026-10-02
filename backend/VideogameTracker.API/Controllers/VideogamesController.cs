using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VideogameTracker.Application.Interfaces;
using VideogameTracker.Domain.Entities;
using VideogameTracker.Application.Features.Videogames.DTOs;

namespace VideogameTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideogamesController : ControllerBase {
    private readonly IVideogameRepository _repository;
    private readonly IValidator<CreateVideogameRequestDto> _validator;

    public VideogamesController(IVideogameRepository repository, IValidator<CreateVideogameRequestDto> validator) {
        _repository = repository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetVideogames() {
        var videogames = await _repository.GetAllAsync();
        
        // Mapeo Manual: Entidad -> DTO
        var response = videogames.Select(v => new VideogameResponseDto {
            Id = v.Id,
            Nombre = v.Nombre
        });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CrearVideogame([FromBody] CreateVideogameRequestDto request) {
        // 1. Ejecutar FluentValidation
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid) {
            return BadRequest(validationResult.Errors); // HTTP 400 automático con detalles
        }

        // 2. Mapeo Manual: DTO -> Entidad (Solo transferimos lo permitido)
        var nuevoVideogame = new VideogameGlobal {
            Nombre = request.Nombre
        };

        // 3. Persistencia
        var videogameCreado = await _repository.AddAsync(nuevoVideogame);
        
        // 4. Mapeo de Retorno
        var response = new VideogameResponseDto {
            Id = videogameCreado.Id,
            Nombre = videogameCreado.Nombre
        };

        return CreatedAtAction(nameof(GetVideogames), new { id = response.Id }, response);
    }
}