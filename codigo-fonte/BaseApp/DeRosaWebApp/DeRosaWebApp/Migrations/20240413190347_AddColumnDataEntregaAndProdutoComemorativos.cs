using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeRosaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnDataEntregaAndProdutoComemorativos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataParaEntregar",
                table: "Pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql("SET IDENTITY_INSERT Categorias ON; INSERT INTO Categorias(IdCategoria, CategoriaNome) VALUES(20, 'Comemorativos')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Produtos]([Nome],[Preco],[Quantidade],[ImagemUrl],[DescricaoCurta],[PrecoSecundario],[IdCategoria])" +
                "VALUES ('BOMBOMZINHOS DÉROSA', 2.50,100,'https://p2.trrsf.com/image/fget/cf/940/0/images.terra.com/2023/03/20/1475660824-bombom-de-nutella.jpg','Bombom caseiro comemorativo, entrega com 7 dias de atencedência' ,2.00,20)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataParaEntregar",
                table: "Pedidos");
        }
    }
}
