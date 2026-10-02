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
        
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid) {
            return BadRequest(validationResult.Errors); 
        }

        
        var nuevoVideogame = new VideogameGlobal {
            Nombre = request.Nombre
        };

        
        var videogameCreado = await _repository.AddAsync(nuevoVideogame);
        
        
        var response = new VideogameResponseDto {
            Id = videogameCreado.Id,
            Nombre = videogameCreado.Nombre
        };

        return CreatedAtAction(nameof(GetVideogames), new { id = response.Id }, response);
    }
}