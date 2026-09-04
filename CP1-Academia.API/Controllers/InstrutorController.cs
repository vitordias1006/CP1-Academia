using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia os instrutores da academia.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class InstrutorController : ControllerBase
{
    private readonly IInstrutorRepository _instrutorRepository;

    public InstrutorController(IInstrutorRepository instrutorRepository)
    {
        _instrutorRepository = instrutorRepository;
    }

    /// <summary>
    /// Lista todos os instrutores cadastrados.
    /// </summary>
    /// <response code="200">Lista de instrutores (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<InstrutorResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var instrutor = _instrutorRepository.GetAll();
        return Ok(instrutor);
    }

    /// <summary>
    /// Busca um instrutor pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do instrutor.</param>
    /// <response code="200">Instrutor encontrado.</response>
    /// <response code="404">Instrutor não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(InstrutorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var instrutor = _instrutorRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(Instrutor), id);

        return Ok(instrutor);
    }

    /// <summary>
    /// Cria um novo instrutor.
    /// </summary>
    /// <param name="request">Dados do instrutor a ser criado.</param>
    /// <response code="200">Criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(InstrutorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] InstrutorRequest request)
    {
        var instrutor = _instrutorRepository.Create(request);
        return Ok(instrutor);
    }

    /// <summary>
    /// Remove um instrutor pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do instrutor.</param>
    /// <response code="204">Removido com sucesso.</response>
    /// <response code="404">Instrutor não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (!_instrutorRepository.Delete(id))
            throw new ResourceNotFoundException(nameof(Instrutor), id);

        return NoContent();
    }
}