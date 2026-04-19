using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
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
    public IActionResult GetAll()
    {
        var localizacao = _localizacaoRespository.GetAll();
        return Ok(localizacao);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var localizacao = _localizacaoRespository.GetById(id);
        if (localizacao is null)
            return NotFound();

        return Ok(localizacao);
    }
    
    [HttpPost]
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