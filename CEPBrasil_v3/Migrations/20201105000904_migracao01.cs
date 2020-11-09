using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CEPBrasil_v3.Migrations
{
    public partial class migracao01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id_Estado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UF = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id_Estado);
                });

            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id_Estado = table.Column<int>(nullable: false),
                    Id_Cidade = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => new { x.Id_Estado, x.Id_Cidade });
                    table.ForeignKey(
                        name: "FK_Cidades_Estados_Id_Estado",
                        column: x => x.Id_Estado,
                        principalTable: "Estados",
                        principalColumn: "Id_Estado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bairros",
                columns: table => new
                {
                    Id_Estado = table.Column<int>(nullable: false),
                    Id_Cidade = table.Column<int>(nullable: false),
                    Id_Bairro = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bairros", x => new { x.Id_Estado, x.Id_Cidade, x.Id_Bairro });
                    table.ForeignKey(
                        name: "FK_Bairros_Cidades_Id_Estado_Id_Cidade",
                        columns: x => new { x.Id_Estado, x.Id_Cidade },
                        principalTable: "Cidades",
                        principalColumns: new[] { "Id_Estado", "Id_Cidade" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logradouros",
                columns: table => new
                {
                    Id_Estado = table.Column<int>(nullable: false),
                    Id_Cidade = table.Column<int>(nullable: false),
                    Id_Bairro = table.Column<int>(nullable: false),
                    Id_Logradouro = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CEP = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logradouros", x => new { x.Id_Estado, x.Id_Cidade, x.Id_Bairro, x.Id_Logradouro });
                    table.ForeignKey(
                        name: "FK_Logradouros_Bairros_Id_Estado_Id_Cidade_Id_Bairro",
                        columns: x => new { x.Id_Estado, x.Id_Cidade, x.Id_Bairro },
                        principalTable: "Bairros",
                        principalColumns: new[] { "Id_Estado", "Id_Cidade", "Id_Bairro" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logradouros");

            migrationBuilder.DropTable(
                name: "Bairros");

            migrationBuilder.DropTable(
                name: "Cidades");

            migrationBuilder.DropTable(
                name: "Estados");
        }
    }
}
