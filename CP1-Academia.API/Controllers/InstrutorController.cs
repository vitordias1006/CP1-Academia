using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstrutorController : ControllerBase
{
    private readonly IInstrutorRepository _instrutorRepository;

    public InstrutorController(IInstrutorRepository instrutorRepository)
    {
        _instrutorRepository = instrutorRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var instrutor = _instrutorRepository.GetAll();
        return Ok(instrutor);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var instrutor = _instrutorRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(Instrutor), id);

        return Ok(instrutor);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Create([FromBody] InstrutorRequest request)
    {
        try
        {
            var instrutor = _instrutorRepository.Create(request);
            return Ok(instrutor);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_instrutorRepository.Delete(id))
            return NotFound();

        return NoContent();
    }
    
    
}