using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RedeAcademiaController : ControllerBase
{
    private readonly IRedeAcademiaRepository _redeAcademiaRepository;

    public RedeAcademiaController(IRedeAcademiaRepository redeAcademiaRepository)
    {
        _redeAcademiaRepository = redeAcademiaRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult GetAll()
    {
        var redeAcademia = _redeAcademiaRepository.GetAll();
        return Ok(redeAcademia);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var redeAcademia = _redeAcademiaRepository.GetById(id)
            ?? throw new ResourceNotFoundException(nameof(RedeAcademia), id);

        return Ok(redeAcademia);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Create([FromBody] RedeAcademiaRequest request)
    {
        try
        {
            var redeAcademia = _redeAcademiaRepository.Create(request);
            return Ok(redeAcademia);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_redeAcademiaRepository.Delete(id))
            return NotFound();

        return NoContent();
    }
    
    
}