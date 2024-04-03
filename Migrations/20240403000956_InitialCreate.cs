using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace desafioLar.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    idPessoa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nmNome = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    nmCPF = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    dtNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    flAtivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.idPessoa);
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    idTelefone = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flTipo = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    nmNumero = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    idPessoa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.idTelefone);
                    table.ForeignKey(
                        name: "FK_Telefone_Pessoa_idPessoa",
                        column: x => x.idPessoa,
                        principalTable: "Pessoa",
                        principalColumn: "idPessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_idPessoa",
                table: "Telefone",
                column: "idPessoa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
