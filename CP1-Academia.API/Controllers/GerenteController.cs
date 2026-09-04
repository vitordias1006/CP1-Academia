using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia os gerentes das unidades da academia.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GerenteConstroller : ControllerBase
{
    private readonly IGerenteRepository _gerenteRepository;

    public GerenteConstroller(IGerenteRepository gerenteRepository)
    {
        _gerenteRepository = gerenteRepository;
    }

    /// <summary>
    /// Lista todos os gerentes cadastrados.
    /// </summary>
    /// <response code="200">Lista de gerentes (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<GerenteResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var gerente = _gerenteRepository.GetAll();
        return Ok(gerente);
    }

    /// <summary>
    /// Busca um gerente pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do gerente.</param>
    /// <response code="200">Gerente encontrado.</response>
    /// <response code="404">Gerente não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GerenteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var gerente = _gerenteRepository.GetById(id)
                      ?? throw new ResourceNotFoundException(nameof(Gerente), id);

        return Ok(gerente);
    }

    /// <summary>
    /// Cria um novo gerente.
    /// </summary>
    /// <param name="request">Dados do gerente a ser criado.</param>
    /// <response code="200">Criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(GerenteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] GerenteRequest request)
    {
        var gerente = _gerenteRepository.Create(request);
        return Ok(gerente);
    }

    /// <summary>
    /// Remove um gerente pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do gerente.</param>
    /// <response code="204">Removido com sucesso.</response>
    /// <response code="404">Gerente não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (!_gerenteRepository.Delete(id))
            throw new ResourceNotFoundException(nameof(Gerente), id);

        return NoContent();
    }
}