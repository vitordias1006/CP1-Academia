using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AulaExtraController : ControllerBase
{
    private readonly IAulaExtraRepository _aulaExtraRepository;

    public AulaExtraController(IAulaExtraRepository aulaExtraRepository)
    {
        _aulaExtraRepository = aulaExtraRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var aulaExtra = _aulaExtraRepository.GetAll();
        return Ok(aulaExtra);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var aulaExtra = _aulaExtraRepository.GetById(id);
        if (aulaExtra is null)
            return NotFound();

        return Ok(aulaExtra);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] AulaExtraRequest request)
    {
        try
        {
            var aulaExtra = _aulaExtraRepository.Create(request);
            return Ok(aulaExtra);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_aulaExtraRepository.Delete(id))
            return NotFound();

        return NoContent();
    }
    
    
}