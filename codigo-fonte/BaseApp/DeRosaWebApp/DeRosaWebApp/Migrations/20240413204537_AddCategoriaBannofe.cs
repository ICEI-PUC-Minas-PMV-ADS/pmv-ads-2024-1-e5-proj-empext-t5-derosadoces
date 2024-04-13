using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeRosaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriaBannofe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Categorias ON;INSERT INTO Categorias (IdCategoria, CategoriaNome) VALUES (1,'Bannofe')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
