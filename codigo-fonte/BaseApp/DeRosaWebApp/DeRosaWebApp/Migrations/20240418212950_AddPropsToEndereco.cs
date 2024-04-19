using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeRosaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPropsToEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rua",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Rua",
                table: "Enderecos");

            migrationBuilder.AlterColumn<int>(
                name: "Cep",
                table: "Pedidos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
