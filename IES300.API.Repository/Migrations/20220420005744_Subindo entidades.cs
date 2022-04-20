using Microsoft.EntityFrameworkCore.Migrations;

namespace IES300.API.Repository.Migrations
{
    public partial class Subindoentidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patrocinador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrocinador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlTabuleiro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPatrocinador = table.Column<int>(type: "int", nullable: false),
                    Ativado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tema_Patrocinador_IdPatrocinador",
                        column: x => x.IdPatrocinador,
                        principalTable: "Patrocinador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ficha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlFicha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTema = table.Column<int>(type: "int", nullable: false),
                    Ativado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ficha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ficha_Tema_IdTema",
                        column: x => x.IdTema,
                        principalTable: "Tema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ficha_IdTema",
                table: "Ficha",
                column: "IdTema");

            migrationBuilder.CreateIndex(
                name: "IX_Patrocinador_Email",
                table: "Patrocinador",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tema_IdPatrocinador",
                table: "Tema",
                column: "IdPatrocinador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ficha");

            migrationBuilder.DropTable(
                name: "Tema");

            migrationBuilder.DropTable(
                name: "Patrocinador");
        }
    }
}
