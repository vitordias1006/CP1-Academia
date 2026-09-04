using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia os planos de assinatura da academia.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PlanoController : ControllerBase
{
    private readonly IPlanoRepository _planoRepository;

    public PlanoController(IPlanoRepository planoRepository)
    {
        _planoRepository = planoRepository;
    }

    /// <summary>
    /// Lista todos os planos cadastrados.
    /// </summary>
    /// <response code="200">Lista de planos (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PlanoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var planos = _planoRepository.GetAll();
        return Ok(planos);
    }

    /// <summary>
    /// Busca um plano pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do plano.</param>
    /// <response code="200">Plano encontrado.</response>
    /// <response code="404">Plano não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PlanoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var plano = _planoRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(Plano), id);

        return Ok(plano);
    }

    /// <summary>
    /// Cria um novo plano.
    /// </summary>
    /// <param name="request">Dados do plano a ser criado.</param>
    /// <response code="200">Criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(PlanoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] PlanoRequest request)
    {
        var plano = _planoRepository.Create(request);
        return Ok(plano);
    }

    /// <summary>
    /// Remove um plano pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do plano.</param>
    /// <response code="204">Removido com sucesso.</response>
    /// <response code="404">Plano não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (!_planoRepository.Delete(id))
            throw new ResourceNotFoundException(nameof(Plano), id);

        return NoContent();
    }
}