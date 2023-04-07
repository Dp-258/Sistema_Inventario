using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCInventario.Migrations
{
    public partial class ClienteProv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CEDULACLIENTE = table.Column<string>(maxLength: 10, nullable: false),
                    NOMBRECLIENTE = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PROVEEDOR",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CEDULAPROVEEDOR = table.Column<string>(maxLength: 10, nullable: false),
                    NOMBREPROVEEDOR = table.Column<string>(maxLength: 50, nullable: false),
                    DIRECCIONPROVEEDOR = table.Column<string>(maxLength: 250, nullable: false),
                    CORREOPROVEEDOR = table.Column<string>(maxLength: 80, nullable: false),
                    CIUDADPROVEEDOR = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROVEEDOR", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTE");

            migrationBuilder.DropTable(
                name: "PROVEEDOR");
        }
    }
}
