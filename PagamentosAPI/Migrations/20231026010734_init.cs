using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PagamentosApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bancos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeDoBanco = table.Column<string>(type: "text", nullable: false),
                    CodigoDoBanco = table.Column<string>(type: "text", nullable: false),
                    PorcentualDeJuros = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boletos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BancoId = table.Column<int>(type: "integer", nullable: false),
                    NomePagador = table.Column<string>(type: "text", nullable: false),
                    CPFOrCNPJ = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    NomeBeneficiario = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "date", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boletos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boletos_Bancos_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Bancos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bancos_CodigoDoBanco",
                table: "Bancos",
                column: "CodigoDoBanco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bancos_NomeDoBanco",
                table: "Bancos",
                column: "NomeDoBanco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_BancoId",
                table: "Boletos",
                column: "BancoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boletos");

            migrationBuilder.DropTable(
                name: "Bancos");
        }
    }
}
