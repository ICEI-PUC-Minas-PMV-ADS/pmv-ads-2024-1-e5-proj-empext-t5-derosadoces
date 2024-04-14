using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeRosaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class NoCheckFkItemCarrinho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE dbo.ItemCarrinhos NOCHECK CONSTRAINT FK_ItemCarrinhos_Pedidos_PedidoCod_Pedido;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE dbo.ItemCarrinhos WITH CHECK CHECK CONSTRAINT FK_ItemCarrinhos_Pedidos_PedidoCod_Pedido;");
        }
    }
}
