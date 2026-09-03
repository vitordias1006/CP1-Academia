using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocalizacaoConstroller : ControllerBase
{
    private readonly ILocalizacaoRespository _localizacaoRespository;

    public LocalizacaoConstroller(ILocalizacaoRespository localizacaoRespository)
    {
        _localizacaoRespository = localizacaoRespository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var localizacao = _localizacaoRespository.GetAll();
        return Ok(localizacao);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var localizacao = _localizacaoRespository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(Localizacao), id);

        return Ok(localizacao);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Create([FromBody] LocalizacaoRequest request)
    {
        try
        {
            var localizacao = _localizacaoRespository.Create(request);
            return Ok(localizacao);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_localizacaoRespository.Delete(id))
            return NotFound();

        return NoContent();
    }
    
    
}