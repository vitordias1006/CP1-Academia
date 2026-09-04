using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia os funcionários da academia.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public FuncionarioController(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    /// <summary>
    /// Lista todos os funcionários cadastrados.
    /// </summary>
    /// <response code="200">Lista de funcionários (pode ser vazia).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<FuncionarioResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var funcionario = _funcionarioRepository.GetAll();
        return Ok(funcionario);
    }

    /// <summary>
    /// Busca um funcionário pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do funcionário.</param>
    /// <response code="200">Funcionário encontrado.</response>
    /// <response code="404">Funcionário não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(FuncionarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var funcionario = _funcionarioRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(Funcionario), id);

        return Ok(funcionario);
    }

    /// <summary>
    /// Cria um novo funcionário.
    /// </summary>
    /// <param name="request">Dados do funcionário a ser criado.</param>
    /// <response code="200">Criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(FuncionarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] FuncionarioRequest request)
    {
        var funcionario = _funcionarioRepository.Create(request);
        return Ok(funcionario);
    }

    /// <summary>
    /// Remove um funcionário pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do funcionário.</param>
    /// <response code="204">Removido com sucesso.</response>
    /// <response code="404">Funcionário não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (!_funcionarioRepository.Delete(id))
            throw new ResourceNotFoundException(nameof(Funcionario), id);

        return NoContent();
    }
}