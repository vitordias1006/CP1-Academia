using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
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
    public IActionResult GetAll()
    {
        var gerente = _gerenteRepository.GetAll();
        return Ok(gerente);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var gerente = _gerenteRepository.GetById(id);
        if (gerente is null)
            return NotFound();

        return Ok(gerente);
    }
    
    [HttpPost]
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