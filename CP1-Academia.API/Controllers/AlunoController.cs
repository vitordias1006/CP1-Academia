using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlunoController : ControllerBase
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoController(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var alunos = _alunoRepository.GetAll();
        return Ok(alunos);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var aluno = _alunoRepository.GetById(id);
        if (aluno is null)
            return NotFound();

        return Ok(aluno);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] AlunoRequest request)
    {
        try
        {
            var aluno = _alunoRepository.Create(request);
            return Ok(aluno);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_alunoRepository.Delete(id))
            return NotFound();

        return NoContent();
    }
    
    
}