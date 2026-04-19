using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
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
    public IActionResult GetAll()
    {
        var fichaTreino = _fichaTreinoRepository.GetAll();
        return Ok(fichaTreino);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var fichaTreino = _fichaTreinoRepository.GetById(id);
        if (fichaTreino is null)
            return NotFound();

        return Ok(fichaTreino);
    }
    
    [HttpPost]
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