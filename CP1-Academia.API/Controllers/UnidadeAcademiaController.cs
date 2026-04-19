using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
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
    public IActionResult GetAll()
    {
        var unidadeAcademia = _unidadeAcademiaRepository.GetAll();
        return Ok(unidadeAcademia);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var unidadeAcademia = _unidadeAcademiaRepository.GetById(id);
        if (unidadeAcademia is null)
            return NotFound();

        return Ok(unidadeAcademia);
    }
    
    [HttpPost]
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