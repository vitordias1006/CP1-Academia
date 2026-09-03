using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlanoController : ControllerBase
{
    private readonly IPlanoRepository _planoRepository;

    public PlanoController(IPlanoRepository planoRepository)
    {
        _planoRepository = planoRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var planos = _planoRepository.GetAll();
        return Ok(planos);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var plano = _planoRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(Plano),id);

        return Ok(plano);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Create([FromBody] PlanoRequest request)
    {
        try
        {
            var plano = _planoRepository.Create(request);
            return Ok(plano);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_planoRepository.Delete(id))
            return NotFound();

        return NoContent();
    }

}