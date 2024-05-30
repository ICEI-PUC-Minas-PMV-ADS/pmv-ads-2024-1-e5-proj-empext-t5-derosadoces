using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeRosaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedMigrationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT ManagementsSobre ON");
            migrationBuilder.Sql(@"
                INSERT INTO ManagementsSobre (Id, TituloSobre, TextoSobre, ImagemUrl) VALUES 
                (1221, 
                 'Bem-vindos à nossa doceria, um lugar onde cada doce é feito à mão com dedicação e muito amor.\r\n        Aqui, revivemos as receitas da vó, preparando doces que tocam o coração e despertam memórias afetivas.', 
                 'Nossos doces são feitos utilizando técnicas artesanais, preservando o sabor autêntico e tradicional que só os doces feitos à mão podem oferecer.\r\n                Em cada receita, em cada mistura, colocamos um pedaço da história e da alma da família, garantindo que cada mordida seja uma experiência única e inesquecível. Valorizamos a qualidade dos ingredientes, a atenção aos detalhes e, principalmente, o amor pelo que fazemos. Por isso, cada doce não só é delicioso, mas também carrega consigo a essência e a paixão da culinária de vó.', 
                 '~/Imagens/julia.png')
            ");
            migrationBuilder.Sql("SET IDENTITY_INSERT ManagementsSobre OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT ManagementsHome ON");
            migrationBuilder.Sql(@"
                INSERT INTO ManagementsHome (Id, TituloSemana) VALUES 
                (1221, 
                 'Produtos da Semana')
            ");
            migrationBuilder.Sql("SET IDENTITY_INSERT ManagementsHome OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ManagementsSobre WHERE Id = 1221");
            migrationBuilder.Sql("DELETE FROM ManagementsHome WHERE Id = 1221");
        }
    }
}
