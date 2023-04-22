using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCInventario.Migrations
{
    public partial class p1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CONTENIDOARCHIVO",
                table: "PRODUCTO");

            migrationBuilder.CreateTable(
                name: "SALIDA",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCLIENTE = table.Column<int>(nullable: false),
                    IDPRODUCTO = table.Column<int>(nullable: false),
                    FECHAREGISTROSALIDA = table.Column<DateTime>(nullable: false),
                    CANTIDADSALIDA = table.Column<int>(nullable: false),
                    MONTOTOTALSALIDA = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALIDA", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SALIDA");

            migrationBuilder.AddColumn<byte[]>(
                name: "CONTENIDOARCHIVO",
                table: "PRODUCTO",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
