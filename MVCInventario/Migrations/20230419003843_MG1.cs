using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCInventario.Migrations
{
    public partial class MG1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ENTRADA",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPROVEEDOR = table.Column<int>(nullable: false),
                    IDPRODUCTO = table.Column<int>(nullable: false),
                    FECHAREGISTROENTRADA = table.Column<DateTime>(nullable: false),
                    CANTIDADPENTRADA = table.Column<int>(nullable: false),
                    MONTOTOTALENTRADA = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTRADA", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTO",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODIGOPRODUCTO = table.Column<string>(nullable: false),
                    NOMBREPRODUCTO = table.Column<string>(maxLength: 50, nullable: false),
                    DESCRIPCIONPRODUCTO = table.Column<string>(nullable: false),
                    STOCKPRODUCTO = table.Column<int>(nullable: false),
                    PVPPRODUCTO = table.Column<decimal>(nullable: false),
                    CATEGORIAPRODUCTO = table.Column<string>(maxLength: 50, nullable: false),
                    CONTENIDOARCHIVO = table.Column<byte[]>(nullable: true),
                    FOTOPRODUCTO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTO", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ENTRADA");

            migrationBuilder.DropTable(
                name: "PRODUCTO");
        }
    }
}
