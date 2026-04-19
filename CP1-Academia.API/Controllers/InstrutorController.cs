using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
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
    public IActionResult GetAll()
    {
        var instrutor = _instrutorRepository.GetAll();
        return Ok(instrutor);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var instrutor = _instrutorRepository.GetById(id);
        if (instrutor is null)
            return NotFound();

        return Ok(instrutor);
    }
    
    [HttpPost]
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