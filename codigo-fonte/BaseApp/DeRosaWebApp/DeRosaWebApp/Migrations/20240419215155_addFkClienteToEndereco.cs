using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeRosaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addFkClienteToEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEndereco",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEndereco",
                table: "Clientes");
        }
    }
}
