using System.ComponentModel.DataAnnotations;
using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public class AlunoRequest
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(150, MinimumLength = 2)]
    public string Nome { get; set; }

    [Required]
    public string Cpf { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Telefone { get; set; }

    [Required]
    public DateTime DataMatricula { get; set; }

    [Required]
    public bool Ativo { get; set; }

    [Required(ErrorMessage = "O identificador do plano é obrigatório")]
    public Guid PlanoId { get; set; }
    
    public Aluno ToDomain() => new Aluno(
        Nome,
        Cpf,
        Email, 
        Telefone, 
        DataMatricula, 
        Ativo,
        PlanoId);
}

