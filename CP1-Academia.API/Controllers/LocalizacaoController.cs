using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia as localizações das unidades da academia.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class LocalizacaoConstroller : ControllerBase
{
    private readonly ILocalizacaoRespository _localizacaoRespository;

    public LocalizacaoConstroller(ILocalizacaoRespository localizacaoRespository)
    {
        _localizacaoRespository = localizacaoRespository;
    }

    /// <summary>
    /// Lista todas as localizações cadastradas.
    /// </summary>
    /// <response code="200">Lista de localizações (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LocalizacaoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var localizacao = _localizacaoRespository.GetAll();
        return Ok(localizacao);
    }

    /// <summary>
    /// Busca uma localização pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da localização.</param>
    /// <response code="200">Localização encontrada.</response>
    /// <response code="404">Localização não encontrada.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LocalizacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var localizacao = _localizacaoRespository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(Localizacao), id);

        return Ok(localizacao);
    }

    /// <summary>
    /// Cria uma nova localização.
    /// </summary>
    /// <param name="request">Dados da localização a ser criada.</param>
    /// <response code="200">Criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(LocalizacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] LocalizacaoRequest request)
    {
        var localizacao = _localizacaoRespository.Create(request);
        return Ok(localizacao);
    }

    /// <summary>
    /// Remove uma localização pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da localização.</param>
    /// <response code="204">Removida com sucesso.</response>
    /// <response code="404">Localização não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (!_localizacaoRespository.Delete(id))
            throw new ResourceNotFoundException(nameof(Localizacao), id);

        return NoContent();
    }
}