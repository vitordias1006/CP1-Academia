using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia os alunos da academia.
/// </summary>
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

    /// <summary>
    /// Lista todos os alunos cadastrados.
    /// </summary>
    /// <response code="200">Lista de alunos (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AlunoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var alunos = _alunoRepository.GetAll();
        return Ok(alunos);
    }

    /// <summary>
    /// Busca um aluno pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do aluno.</param>
    /// <response code="200">Aluno encontrado.</response>
    /// <response code="404">Aluno não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AlunoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var aluno = _alunoRepository.GetById(id)
                    ?? throw new ResourceNotFoundException(nameof(Aluno), id);

        return Ok(aluno);
    }

    /// <summary>
    /// Cria um novo aluno.
    /// </summary>
    /// <param name="request">Dados do aluno a ser criado.</param>
    /// <response code="200">Aluno criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(AlunoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] AlunoRequest request)
    {
        var aluno = _alunoRepository.Create(request);
        return Ok(aluno);
    }

    /// <summary>
    /// Remove um aluno pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do aluno.</param>
    /// <response code="204">Aluno removido com sucesso.</response>
    /// <response code="404">Aluno não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (!_alunoRepository.Delete(id))
            throw new ResourceNotFoundException(nameof(Aluno), id);

        return NoContent();
    }

    /// <summary>
    /// Remove um aluno usando o repositório genérico (demonstração do IRepository&lt;T&gt;).
    /// </summary>
    /// <param name="id">Identificador do aluno.</param>
    /// <response code="204">Aluno removido com sucesso.</response>
    /// <response code="404">Aluno não encontrado.</response>
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