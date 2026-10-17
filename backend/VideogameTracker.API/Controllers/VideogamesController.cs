using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VideogameTracker.Application.Features.Videogames.DTOs;
using VideogameTracker.Application.Interfaces;
using VideogameTracker.Domain.Entities;

namespace VideogameTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideogamesController : ControllerBase 
{
    private readonly IVideogameRepository _repository;
    private readonly IValidator<CreateVideogameRequestDto> _createValidator;
    private readonly IValidator<UpdateVideogameRequestDto> _updateValidator;

    public VideogamesController(
        IVideogameRepository repository, 
        IValidator<CreateVideogameRequestDto> createValidator,
        IValidator<UpdateVideogameRequestDto> updateValidator) 
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetVideogames() 
    {
        var videogames = await _repository.GetAllAsync();
        
        var response = videogames.Select(v => new VideogameResponseDto 
        {
            Id = v.Id,
            Nombre = v.Nombre
        });

        return Ok(response);
    }

    // Nuevo: GET por ID (Retorna 404 si no existe)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVideogameById(int id)
    {
        var videogame = await _repository.GetByIdAsync(id);
        if (videogame == null)
        {
            return NotFound(new { mensaje = $"No se encontró el videojuego con el ID {id}." });
        }

        var response = new VideogameResponseDto
        {
            Id = videogame.Id,
            Nombre = videogame.Nombre
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CrearVideogame([FromBody] CreateVideogameRequestDto request) 
    {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid) 
        {
            return BadRequest(validationResult.Errors); 
        }

        var nuevoVideogame = new VideogameGlobal 
        {
            Nombre = request.Nombre
        };

        var videogameCreado = await _repository.AddAsync(nuevoVideogame);
        
        var response = new VideogameResponseDto 
        {
            Id = videogameCreado.Id,
            Nombre = videogameCreado.Nombre
        };

        return CreatedAtAction(nameof(GetVideogameById), new { id = response.Id }, response);
    }

    // Nuevo: PUT (Actualizar registro)
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarVideogame(int id, [FromBody] UpdateVideogameRequestDto request)
    {
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var videogameExistente = await _repository.GetByIdAsync(id);
        if (videogameExistente == null)
        {
            return NotFound(new { mensaje = $"No se encontró el videojuego con el ID {id} para actualizar." });
        }

        videogameExistente.Nombre = request.Nombre;

        await _repository.UpdateAsync(videogameExistente);

        return NoContent(); // HTTP 204 exitoso sin contenido
    }

    // Nuevo: DELETE (Eliminar registro)
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarVideogame(int id)
    {
        var videogameExistente = await _repository.GetByIdAsync(id);
        if (videogameExistente == null)
        {
            return NotFound(new { mensaje = $"No se encontró el videojuego con el ID {id} para eliminar." });
        }

        await _repository.DeleteAsync(videogameExistente);

        return NoContent(); // HTTP 204 exitoso sin contenido
    }
}