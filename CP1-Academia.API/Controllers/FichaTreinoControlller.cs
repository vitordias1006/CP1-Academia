using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FichaTreinoController : ControllerBase
{
    private readonly IFichaTreinoRepository _fichaTreinoRepository;

    public FichaTreinoController(IFichaTreinoRepository fichaTreinoRepository)
    {
        _fichaTreinoRepository = fichaTreinoRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var fichaTreino = _fichaTreinoRepository.GetAll();
        return Ok(fichaTreino);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var fichaTreino = _fichaTreinoRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(FichaTreino), id);

        return Ok(fichaTreino);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Create([FromBody] FichaTreinoRequest request)
    {
        try
        {
            var fichaTreino = _fichaTreinoRepository.Create(request);
            return Ok(fichaTreino);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_fichaTreinoRepository.Delete(id))
            return NotFound();

        return NoContent();
    }
    
    
}