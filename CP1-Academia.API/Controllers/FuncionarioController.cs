using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public FuncionarioController(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var funcionario = _funcionarioRepository.GetAll();
        return Ok(funcionario);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var funcionario = _funcionarioRepository.GetById(id);
        if (funcionario is null)
            return NotFound();

        return Ok(funcionario);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] FuncionarioRequest request)
    {
        try
        {
            var funcionario = _funcionarioRepository.Create(request);
            return Ok(funcionario);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_funcionarioRepository.Delete(id))
            return NotFound();

        return NoContent();
    }
    
    
}