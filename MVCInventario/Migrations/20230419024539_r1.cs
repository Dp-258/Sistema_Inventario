using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCInventario.Migrations
{
    public partial class r1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CONTENIDOARCHIVO",
                table: "PRODUCTO",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CONTENIDOARCHIVO",
                table: "PRODUCTO");
        }
    }
}
