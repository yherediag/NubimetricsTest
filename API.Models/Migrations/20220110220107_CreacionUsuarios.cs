using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Models.Migrations
{
    public partial class CreacionUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(31)", unicode: false, maxLength: 30, nullable: false),
                    Apellido = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    FechaAlta = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
