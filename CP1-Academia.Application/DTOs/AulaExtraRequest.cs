using System.ComponentModel.DataAnnotations;

namespace CP1_Application.DTOs;

public record AulaExtraRequest(
    [property: Required(ErrorMessage = "O tipo de aula é obrigatório")]
    [property: StringLength(100, MinimumLength = 2, ErrorMessage = "O tipo de aula deve ter entre 2 e 100 caracteres")]
    string TipoDeAula,

    [property: Required(ErrorMessage = "O horário da aula é obrigatório")]
    [property: Range(typeof(DateTime), "2000-01-01", "2100-12-31", ErrorMessage = "O horário deve estar entre 2000 e 2100")]
    DateTime HorarioAula,

    [property: Required(ErrorMessage = "A capacidade é obrigatória")]
    [property: Range(1, 200, ErrorMessage = "A capacidade deve estar entre 1 e 200 alunos")]
    int Capacidade,

    [property: Required(ErrorMessage = "O identificador da ficha de treino é obrigatório")]
    Guid FichaTreinoId);
