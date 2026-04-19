using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
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
    public IActionResult GetAll()
    {
        var redeAcademia = _redeAcademiaRepository.GetAll();
        return Ok(redeAcademia);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var redeAcademia = _redeAcademiaRepository.GetById(id);
        if (redeAcademia is null)
            return NotFound();

        return Ok(redeAcademia);
    }
    
    [HttpPost]
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