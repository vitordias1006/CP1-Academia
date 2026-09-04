using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia as redes de academia.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RedeAcademiaController : ControllerBase
{
    private readonly IRedeAcademiaRepository _redeAcademiaRepository;

    public RedeAcademiaController(IRedeAcademiaRepository redeAcademiaRepository)
    {
        _redeAcademiaRepository = redeAcademiaRepository;
    }

    /// <summary>
    /// Lista todas as redes de academia cadastradas.
    /// </summary>
    /// <response code="200">Lista de redes (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RedeAcademiaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var redeAcademia = _redeAcademiaRepository.GetAll();
        return Ok(redeAcademia);
    }

    /// <summary>
    /// Busca uma rede de academia pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da rede de academia.</param>
    /// <response code="200">Rede encontrada.</response>
    /// <response code="404">Rede não encontrada.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RedeAcademiaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var redeAcademia = _redeAcademiaRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(RedeAcademia), id);

        return Ok(redeAcademia);
    }

    /// <summary>
    /// Cria uma nova rede de academia.
    /// </summary>
    /// <param name="request">Dados da rede de academia a ser criada.</param>
    /// <response code="200">Criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(RedeAcademiaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] RedeAcademiaRequest request)
    {
        var redeAcademia = _redeAcademiaRepository.Create(request);
        return Ok(redeAcademia);
    }

    /// <summary>
    /// Remove uma rede de academia pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da rede de academia.</param>
    /// <response code="204">Removida com sucesso.</response>
    /// <response code="404">Rede não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (!_redeAcademiaRepository.Delete(id))
            throw new ResourceNotFoundException(nameof(RedeAcademia), id);

        return NoContent();
    }
}