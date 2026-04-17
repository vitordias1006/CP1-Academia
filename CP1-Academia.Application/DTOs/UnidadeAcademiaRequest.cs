using System.ComponentModel.DataAnnotations;

namespace CP1_Application.DTOs;

public record UnidadeAcademiaRequest(
    [property: Required(ErrorMessage = "O telefone é obrigatório")]
    [property: RegularExpression(@"^\d{10,11}$", ErrorMessage = "O telefone deve conter entre 10 e 11 dígitos numéricos")]
    string Telefone,

    [property: Required(ErrorMessage = "O campo ativo é obrigatório")]
    bool Ativo,

    [property: Required(ErrorMessage = "O horário de funcionamento é obrigatório")]
    [property: Range(typeof(DateTime), "2000-01-01", "2100-12-31", ErrorMessage = "O horário de funcionamento deve estar entre 2000 e 2100")]
    DateTime HorarioFuncionamento,

    [property: Required(ErrorMessage = "O identificador da rede é obrigatório")]
    Guid RedeAcademiaId,

    [property: Required(ErrorMessage = "O identificador do gerente é obrigatório")]
    Guid GerenteId,

    [property: Required(ErrorMessage = "O identificador da localização é obrigatório")]
    Guid LocalizacaoId);
