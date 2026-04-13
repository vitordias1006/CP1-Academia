using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CP1_Academia.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AulaExtras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TipoDeAula = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HorarioAula = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Capacidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FichaTreinoId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AulaExtras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Preco = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DataDeAssinatura = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataDeRenovacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TipoPlano = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    Fidelidade = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    Ativo = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RedeAcademia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    QntdUnidades = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Cnpj = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataFundacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedeAcademia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    DataMatricula = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Ativo = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    PlanoId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PlanoId1 = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Planos_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alunos_Planos_PlanoId1",
                        column: x => x.PlanoId1,
                        principalTable: "Planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaTreino",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Exercicios = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    Repeticoes = table.Column<int>(type: "NUMBER(10)", maxLength: 2, nullable: false),
                    Series = table.Column<int>(type: "NUMBER(10)", maxLength: 2, nullable: false),
                    TipoExercicio = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    MusculoAlvo = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    Observacao = table.Column<string>(type: "NVARCHAR2(400)", maxLength: 400, nullable: false),
                    AlunoId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaTreino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichaTreino_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaTreinoAulasExtras",
                columns: table => new
                {
                    AulaExtrasId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    FichaTreinosId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaTreinoAulasExtras", x => new { x.AulaExtrasId, x.FichaTreinosId });
                    table.ForeignKey(
                        name: "FK_FichaTreinoAulasExtras_AulaExtras_AulaExtrasId",
                        column: x => x.AulaExtrasId,
                        principalTable: "AulaExtras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaTreinoAulasExtras_FichaTreino_FichaTreinosId",
                        column: x => x.FichaTreinosId,
                        principalTable: "FichaTreino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Cargo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    GerenteId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Salario = table.Column<double>(type: "BINARY_DOUBLE", maxLength: 5, nullable: false),
                    DataDeContratacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Ativo = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    UnidadeAcademiaId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gerentes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Comissao = table.Column<double>(type: "BINARY_DOUBLE", maxLength: 5, nullable: false),
                    PeriodoDeLideranca = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    AreaDeResponsabilidade = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    NivelDeLideranca = table.Column<string>(type: "NVARCHAR2(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gerentes_Funcionarios_Id",
                        column: x => x.Id,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instrutor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Cref = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instrutor_Funcionarios_Id",
                        column: x => x.Id,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localizacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Bairro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Rua = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Numero = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UnidadeAcademiaId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeAcademia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Ativo = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    HorarioFuncionamento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    RedeAcademiaId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    GerenteId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    LocalizacaoId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    LocalizacaoId1 = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeAcademia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadeAcademia_Gerentes_GerenteId",
                        column: x => x.GerenteId,
                        principalTable: "Gerentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnidadeAcademia_Localizacao_LocalizacaoId1",
                        column: x => x.LocalizacaoId1,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnidadeAcademia_RedeAcademia_RedeAcademiaId",
                        column: x => x.RedeAcademiaId,
                        principalTable: "RedeAcademia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_PlanoId",
                table: "Alunos",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_PlanoId1",
                table: "Alunos",
                column: "PlanoId1");

            migrationBuilder.CreateIndex(
                name: "IX_FichaTreino_AlunoId",
                table: "FichaTreino",
                column: "AlunoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FichaTreinoAulasExtras_FichaTreinosId",
                table: "FichaTreinoAulasExtras",
                column: "FichaTreinosId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_GerenteId",
                table: "Funcionarios",
                column: "GerenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_UnidadeAcademiaId",
                table: "Funcionarios",
                column: "UnidadeAcademiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacao_UnidadeAcademiaId",
                table: "Localizacao",
                column: "UnidadeAcademiaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeAcademia_GerenteId",
                table: "UnidadeAcademia",
                column: "GerenteId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeAcademia_LocalizacaoId1",
                table: "UnidadeAcademia",
                column: "LocalizacaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeAcademia_RedeAcademiaId",
                table: "UnidadeAcademia",
                column: "RedeAcademiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Gerentes_GerenteId",
                table: "Funcionarios",
                column: "GerenteId",
                principalTable: "Gerentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_UnidadeAcademia_UnidadeAcademiaId",
                table: "Funcionarios",
                column: "UnidadeAcademiaId",
                principalTable: "UnidadeAcademia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Localizacao_UnidadeAcademia_UnidadeAcademiaId",
                table: "Localizacao",
                column: "UnidadeAcademiaId",
                principalTable: "UnidadeAcademia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Gerentes_GerenteId",
                table: "Funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_UnidadeAcademia_Gerentes_GerenteId",
                table: "UnidadeAcademia");

            migrationBuilder.DropForeignKey(
                name: "FK_Localizacao_UnidadeAcademia_UnidadeAcademiaId",
                table: "Localizacao");

            migrationBuilder.DropTable(
                name: "FichaTreinoAulasExtras");

            migrationBuilder.DropTable(
                name: "Instrutor");

            migrationBuilder.DropTable(
                name: "AulaExtras");

            migrationBuilder.DropTable(
                name: "FichaTreino");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Planos");

            migrationBuilder.DropTable(
                name: "Gerentes");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "UnidadeAcademia");

            migrationBuilder.DropTable(
                name: "Localizacao");

            migrationBuilder.DropTable(
                name: "RedeAcademia");
        }
    }
}
