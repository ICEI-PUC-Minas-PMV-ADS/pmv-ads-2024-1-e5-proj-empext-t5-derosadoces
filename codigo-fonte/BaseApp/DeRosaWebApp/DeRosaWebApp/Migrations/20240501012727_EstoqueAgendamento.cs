using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeRosaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class EstoqueAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagemUrl",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstoqueAgendamento",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstoqueAgendamento",
                table: "Produtos");

       

            migrationBuilder.AlterColumn<string>(
                name: "ImagemUrl",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
