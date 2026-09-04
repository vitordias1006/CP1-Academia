using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia as aulas extras oferecidas pela academia.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AulaExtraController : ControllerBase
{
    private readonly IAulaExtraRepository _aulaExtraRepository;

    public AulaExtraController(IAulaExtraRepository aulaExtraRepository)
    {
        _aulaExtraRepository = aulaExtraRepository;
    }

    /// <summary>
    /// Lista todas as aulas extras cadastradas.
    /// </summary>
    /// <response code="200">Lista de aulas extras (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AulaExtraResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var aulaExtra = _aulaExtraRepository.GetAll();
        return Ok(aulaExtra);
    }

    /// <summary>
    /// Busca uma aula extra pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da aula extra.</param>
    /// <response code="200">Aula extra encontrada.</response>
    /// <response code="404">Aula extra não encontrada.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AulaExtraResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var aulaExtra = _aulaExtraRepository.GetById(id)
                        ?? throw new ResourceNotFoundException(nameof(AulaExtra), id);

        return Ok(aulaExtra);
    }

    /// <summary>
    /// Cria uma nova aula extra.
    /// </summary>
    /// <param name="request">Dados da aula extra a ser criada.</param>
    /// <response code="200">Criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(AulaExtraResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] AulaExtraRequest request)
    {
        var aulaExtra = _aulaExtraRepository.Create(request);
        return Ok(aulaExtra);
    }

    /// <summary>
    /// Remove uma aula extra pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da aula extra.</param>
    /// <response code="204">Removida com sucesso.</response>
    /// <response code="404">Aula extra não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (!_aulaExtraRepository.Delete(id))
            throw new ResourceNotFoundException(nameof(AulaExtra), id);

        return NoContent();
    }
}