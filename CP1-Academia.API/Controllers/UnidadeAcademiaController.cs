using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnidadeAcademiaConstroller : ControllerBase
{
    private readonly IUnidadeAcademiaRepository _unidadeAcademiaRepository;

    public UnidadeAcademiaConstroller(IUnidadeAcademiaRepository unidadeAcademiaRepository)
    {
        _unidadeAcademiaRepository = unidadeAcademiaRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var unidadeAcademia = _unidadeAcademiaRepository.GetAll();
        return Ok(unidadeAcademia);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var unidadeAcademia = _unidadeAcademiaRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(UnidadeAcademia), id);
        
        return Ok(unidadeAcademia);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Create([FromBody] UnidadeAcademiaRequest request)
    {
        try
        {
            var unidadeAcademia = _unidadeAcademiaRepository.Create(request);
            return Ok(unidadeAcademia);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_unidadeAcademiaRepository.Delete(id))
            return NotFound();

        return NoContent();
    }
    
    
}