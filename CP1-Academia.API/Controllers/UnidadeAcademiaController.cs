using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia as unidades da academia.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UnidadeAcademiaConstroller : ControllerBase
{
    private readonly IUnidadeAcademiaRepository _unidadeAcademiaRepository;

    public UnidadeAcademiaConstroller(IUnidadeAcademiaRepository unidadeAcademiaRepository)
    {
        _unidadeAcademiaRepository = unidadeAcademiaRepository;
    }

    /// <summary>
    /// Lista todas as unidades de academia cadastradas.
    /// </summary>
    /// <response code="200">Lista de unidades (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<UnidadeAcademiaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var unidadeAcademia = _unidadeAcademiaRepository.GetAll();
        return Ok(unidadeAcademia);
    }

    /// <summary>
    /// Busca uma unidade de academia pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da unidade de academia.</param>
    /// <response code="200">Unidade encontrada.</response>
    /// <response code="404">Unidade não encontrada.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UnidadeAcademiaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var unidadeAcademia = _unidadeAcademiaRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(UnidadeAcademia), id);

        return Ok(unidadeAcademia);
    }

    /// <summary>
    /// Cria uma nova unidade de academia.
    /// </summary>
    /// <param name="request">Dados da unidade a ser criada.</param>
    /// <response code="200">Criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(UnidadeAcademiaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] UnidadeAcademiaRequest request)
    {
        var unidadeAcademia = _unidadeAcademiaRepository.Create(request);
        return Ok(unidadeAcademia);
    }

    /// <summary>
    /// Remove uma unidade de academia pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da unidade de academia.</param>
    /// <response code="204">Removida com sucesso.</response>
    /// <response code="404">Unidade não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (!_unidadeAcademiaRepository.Delete(id))
            throw new ResourceNotFoundException(nameof(UnidadeAcademia), id);

        return NoContent();
    }
}