using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCInventario.Migrations
{
    public partial class ProductoAct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PVPPRODUCTO",
                table: "PRODUCTO",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PVPPRODUCTO",
                table: "PRODUCTO",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
