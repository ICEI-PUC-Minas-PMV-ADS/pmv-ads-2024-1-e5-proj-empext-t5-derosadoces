using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeRosaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Atualização : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCarrinho",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "ClienteCod_Cliente",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeUsuario",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteCod_Cliente",
                table: "Pedidos",
                column: "ClienteCod_Cliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteCod_Cliente",
                table: "Pedidos",
                column: "ClienteCod_Cliente",
                principalTable: "Clientes",
                principalColumn: "Cod_Cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteCod_Cliente",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ClienteCod_Cliente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ClienteCod_Cliente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "NomeUsuario",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "IdCarrinho",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
