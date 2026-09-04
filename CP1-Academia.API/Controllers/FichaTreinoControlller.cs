using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia as fichas de treino dos alunos.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FichaTreinoController : ControllerBase
{
    private readonly IFichaTreinoRepository _fichaTreinoRepository;

    public FichaTreinoController(IFichaTreinoRepository fichaTreinoRepository)
    {
        _fichaTreinoRepository = fichaTreinoRepository;
    }

    /// <summary>
    /// Lista todas as fichas de treino cadastradas.
    /// </summary>
    /// <response code="200">Lista de fichas (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<FichaTreinoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var fichaTreino = _fichaTreinoRepository.GetAll();
        return Ok(fichaTreino);
    }

    /// <summary>
    /// Busca uma ficha de treino pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da ficha de treino.</param>
    /// <response code="200">Ficha encontrada.</response>
    /// <response code="404">Ficha não encontrada.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(FichaTreinoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var fichaTreino = _fichaTreinoRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(FichaTreino), id);

        return Ok(fichaTreino);
    }

    /// <summary>
    /// Cria uma nova ficha de treino.
    /// </summary>
    /// <param name="request">Dados da ficha de treino a ser criada.</param>
    /// <response code="200">Criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(FichaTreinoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] FichaTreinoRequest request)
    {
        var fichaTreino = _fichaTreinoRepository.Create(request);
        return Ok(fichaTreino);
    }

    /// <summary>
    /// Remove uma ficha de treino pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da ficha de treino.</param>
    /// <response code="204">Removida com sucesso.</response>
    /// <response code="404">Ficha não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (!_fichaTreinoRepository.Delete(id))
            throw new ResourceNotFoundException(nameof(FichaTreino), id);

        return NoContent();
    }
}   