using Asp.Versioning;
using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.API.RateLimiting;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CP1_Academia.API.Controllers;

/// <summary>
/// Gerencia os alunos da academia.
/// </summary>
[ApiVersion("1.0", Deprecated = true)]
[ApiVersion("2.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AlunoController : ControllerBase
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IRepository<Aluno> _repository;
    private readonly ILogger<AlunoController> _logger;

    public AlunoController(IAlunoRepository alunoRepository, IRepository<Aluno> repository, ILogger<AlunoController> logger)
    {
        _alunoRepository = alunoRepository;
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// [v1.0 — DEPRECADA] Lista todos os alunos (array simples, sem paginação).
    /// </summary>
    /// <remarks>Use a v2.0, que devolve um envelope paginado.</remarks>
    /// <response code="200">Lista de alunos (pode ser vazia).</response>
    [HttpGet]
    [MapToApiVersion("1.0")]
    [Obsolete("Versão 1.0 deprecada. Use a versão 2.0 (paginada).")]
    [ProducesResponseType(typeof(IReadOnlyList<AlunoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var alunos = _alunoRepository.GetAll();
        return Ok(alunos);
    }

    /// <summary>
    /// [v2.0] Lista os alunos de forma paginada.
    /// </summary>
    /// <param name="page">Página (>= 1). Padrão: 1.</param>
    /// <param name="pageSize">Itens por página (1 a 100). Padrão: 20.</param>
    /// <response code="200">Envelope com a página, totais e itens (items vazio se a página passar do total).</response>
    /// <response code="400">page ou pageSize fora da faixa permitida.</response>
    [HttpGet]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(typeof(PagedResult<AlunoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetPaged(
        [FromQuery] int page = PageRequest.DefaultPage,
        [FromQuery] int pageSize = PageRequest.DefaultPageSize)
    {
        // Application valida (lança ArgumentException => GlobalExceptionHandler => 400 Problem Details)
        var pageRequest = PageRequest.Create(page, pageSize);

        var result = _alunoRepository.GetPaged(pageRequest);
        return Ok(result);
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
    /// Cria um novo aluno. Limitado a 10 requisições por minuto por IP.
    /// </summary>
    /// <param name="request">Dados do aluno a ser criado.</param>
    /// <response code="200">Aluno criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="429">Limite de requisições excedido (veja o header Retry-After).</response>
    [HttpPost]
    [EnableRateLimiting(RateLimitPolicies.Escrita)]
    [ProducesResponseType(typeof(AlunoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    public IActionResult Create([FromBody] AlunoRequest request)
    {
        var traceId = HttpContext.TraceIdentifier;

        _logger.LogInformation(
            "Iniciando criação de aluno. Nome: {Nome}, TraceId: {TraceId}",
            request.Nome, traceId);

        var aluno = _alunoRepository.Create(request);

        _logger.LogInformation(
            "Aluno criado com sucesso. AlunoId: {AlunoId}, TraceId: {TraceId}",
            aluno.Id, traceId);
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