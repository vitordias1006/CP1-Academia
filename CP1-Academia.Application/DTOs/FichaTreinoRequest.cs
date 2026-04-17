using System.ComponentModel.DataAnnotations;

namespace CP1_Academia.API.Application.DTOs;

public record FichaTreinoRequest(
    [property: Required(ErrorMessage = "Os exercícios são obrigatórios")]
    [property: StringLength(500, MinimumLength = 2, ErrorMessage = "Os exercícios devem ter entre 2 e 500 caracteres")]
    string Exercicios,

    [property: Required(ErrorMessage = "O número de repetições é obrigatório")]
    [property: Range(1, 500, ErrorMessage = "As repetições devem estar entre 1 e 500")]
    int Repeticoes,

    [property: Required(ErrorMessage = "O número de séries é obrigatório")]
    [property: Range(1, 50, ErrorMessage = "As séries devem estar entre 1 e 50")]
    int Series,

    [property: Required(ErrorMessage = "O tipo de exercício é obrigatório")]
    [property: StringLength(100, MinimumLength = 2, ErrorMessage = "O tipo de exercício deve ter entre 2 e 100 caracteres")]
    string TipoExercicio,

    [property: Required(ErrorMessage = "O músculo alvo é obrigatório")]
    [property: StringLength(100, MinimumLength = 2, ErrorMessage = "O músculo alvo deve ter entre 2 e 100 caracteres")]
    string MusculoAlvo,

    [property: StringLength(500, ErrorMessage = "A observação deve ter no máximo 500 caracteres")]
    string? Observacao,

    [property: Required(ErrorMessage = "O identificador do aluno é obrigatório")]
    Guid AlunoId);
