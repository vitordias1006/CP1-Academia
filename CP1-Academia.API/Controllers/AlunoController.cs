using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlunoController : ControllerBase
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IRepository<Aluno> _repository;

    public AlunoController(IAlunoRepository alunoRepository, IRepository<Aluno> repository)
    {
        _alunoRepository = alunoRepository;
        _repository = repository;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(AlunoResponse), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var alunos = _alunoRepository.GetAll();
        return Ok(alunos);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AlunoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var aluno = _alunoRepository.GetById(id)
                    ?? throw new ResourceNotFoundException(nameof(Aluno), id);

        return Ok(aluno);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(AlunoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] AlunoRequest request)
    {
        var aluno = _alunoRepository.Create(request);
        return Ok(aluno);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_alunoRepository.Delete(id))
            return NotFound();

        return NoContent();
    }
    
    [HttpDelete("generico/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteGenerico(Guid id)
    {
        if (!_repository.ExistsById(id))
            throw new ResourceNotFoundException(nameof(Aluno), id);

        _repository.Delete(id);
        return NoContent();
    }
}