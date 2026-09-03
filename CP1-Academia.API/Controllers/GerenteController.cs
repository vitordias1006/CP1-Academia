using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GerenteConstroller : ControllerBase
{
    private readonly IGerenteRepository _gerenteRepository;

    public GerenteConstroller(IGerenteRepository gerenteRepository)
    {
        _gerenteRepository = gerenteRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var gerente = _gerenteRepository.GetAll();
        return Ok(gerente);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var gerente = _gerenteRepository.GetById(id)
                      ?? throw new ResourceNotFoundException(nameof(Gerente), id);

        return Ok(gerente);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Create([FromBody] GerenteRequest request)
    {
        try
        {
            var gerente = _gerenteRepository.Create(request);
            return Ok(gerente);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_gerenteRepository.Delete(id))
            return NotFound();

        return NoContent();
    }
    
    
}