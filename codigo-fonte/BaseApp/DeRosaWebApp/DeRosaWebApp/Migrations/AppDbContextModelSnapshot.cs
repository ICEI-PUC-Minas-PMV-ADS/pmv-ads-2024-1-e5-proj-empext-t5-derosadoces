﻿// <auto-generated />
using System;
using DeRosaWebApp.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeRosaWebApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DeRosaWebApp.Models.Carrinho", b =>
                {
                    b.Property<string>("Cod_Carrinho")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("QuantidadeTotal")
                        .HasColumnType("int");

                    b.HasKey("Cod_Carrinho");

                    b.ToTable("Carrinhos");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategoria"));

                    b.Property<string>("CategoriaNome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdCategoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Cliente", b =>
                {
                    b.Property<int>("Cod_Cliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cod_Cliente"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("DateNasc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdEndereco")
                        .HasColumnType("int");

                    b.Property<string>("Id_User")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NomeUsuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("_CarrinhoCod_Carrinho")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Cod_Cliente");

                    b.HasIndex("_CarrinhoCod_Carrinho");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int?>("ClienteCod_Cliente")
                        .HasColumnType("int");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Id_User")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteCod_Cliente");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Entities.Admin", b =>
                {
                    b.Property<string>("Cod_Admin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome_Fantasia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cod_Admin");

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.ItemCarrinho", b =>
                {
                    b.Property<string>("Cod_ItemCarrinho")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CarrinhoCod_Carrinho")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Cod_Carrinho")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PedidoCod_Pedido")
                        .HasColumnType("int");

                    b.Property<int?>("ProdutoCod_Produto")
                        .HasColumnType("int");

                    b.Property<int>("QntProduto")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.HasKey("Cod_ItemCarrinho");

                    b.HasIndex("CarrinhoCod_Carrinho");

                    b.HasIndex("PedidoCod_Pedido");

                    b.HasIndex("ProdutoCod_Produto");

                    b.ToTable("ItemCarrinhos");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Management.ManagementHome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TituloSemana")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ManagementsHome");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Management.ManagementSobre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagemUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextoSobre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TituloSobre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ManagementsSobre");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Pedido", b =>
                {
                    b.Property<int>("Cod_Pedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cod_Pedido"));

                    b.Property<bool>("Agendado")
                        .HasColumnType("bit");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ClienteCod_Cliente")
                        .HasColumnType("int");

                    b.Property<string>("Complemento")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Concluido")
                        .HasColumnType("bit");

                    b.Property<string>("Conjunto_IdProdutos")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("DataExpiracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataParaEntregar")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Entregue")
                        .HasColumnType("bit");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<double>("Frete")
                        .HasColumnType("float");

                    b.Property<string>("Id_User")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<bool>("Pago")
                        .HasColumnType("bit");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int>("TotalItensPedido")
                        .HasColumnType("int");

                    b.Property<double>("TotalPedido")
                        .HasColumnType("float");

                    b.HasKey("Cod_Pedido");

                    b.HasIndex("ClienteCod_Cliente");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.PedidoDetalhe", b =>
                {
                    b.Property<int>("Cod_PedidoDetalhe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cod_PedidoDetalhe"));

                    b.Property<int>("Cod_Pedido")
                        .HasColumnType("int");

                    b.Property<int>("Cod_Produto")
                        .HasColumnType("int");

                    b.Property<string>("Conjunto_Pedidos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_User")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PedidoCod_Pedido")
                        .HasColumnType("int");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProdutoCod_Produto")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Cod_PedidoDetalhe");

                    b.HasIndex("PedidoCod_Pedido");

                    b.HasIndex("ProdutoCod_Produto");

                    b.ToTable("PedidoDetalhes");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Produto", b =>
                {
                    b.Property<int>("Cod_Produto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cod_Produto"));

                    b.Property<string>("DescricaoCurta")
                        .IsRequired()
                        .HasMaxLength(208)
                        .HasColumnType("nvarchar(208)");

                    b.Property<int>("EmEstoque")
                        .HasColumnType("int");

                    b.Property<int>("EstoqueAgendamento")
                        .HasColumnType("int");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int");

                    b.Property<string>("ImagemUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Indisponivel")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.Property<decimal>("PrecoSecundario")
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("ProdutoDaSemana")
                        .HasColumnType("bit");

                    b.Property<bool>("Promocional")
                        .HasColumnType("bit");

                    b.HasKey("Cod_Produto");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Cliente", b =>
                {
                    b.HasOne("DeRosaWebApp.Models.Carrinho", "_Carrinho")
                        .WithMany()
                        .HasForeignKey("_CarrinhoCod_Carrinho");

                    b.Navigation("_Carrinho");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Endereco", b =>
                {
                    b.HasOne("DeRosaWebApp.Models.Cliente", null)
                        .WithMany("_Enderecos")
                        .HasForeignKey("ClienteCod_Cliente");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.ItemCarrinho", b =>
                {
                    b.HasOne("DeRosaWebApp.Models.Carrinho", null)
                        .WithMany("ListItemCarrinho")
                        .HasForeignKey("CarrinhoCod_Carrinho");

                    b.HasOne("DeRosaWebApp.Models.Pedido", null)
                        .WithMany("ProdutosPedido")
                        .HasForeignKey("PedidoCod_Pedido");

                    b.HasOne("DeRosaWebApp.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoCod_Produto");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Pedido", b =>
                {
                    b.HasOne("DeRosaWebApp.Models.Cliente", null)
                        .WithMany("_Pedidos")
                        .HasForeignKey("ClienteCod_Cliente");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.PedidoDetalhe", b =>
                {
                    b.HasOne("DeRosaWebApp.Models.Pedido", "Pedido")
                        .WithMany("_PedidoDetalhes")
                        .HasForeignKey("PedidoCod_Pedido");

                    b.HasOne("DeRosaWebApp.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoCod_Produto");

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Carrinho", b =>
                {
                    b.Navigation("ListItemCarrinho");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Cliente", b =>
                {
                    b.Navigation("_Enderecos");

                    b.Navigation("_Pedidos");
                });

            modelBuilder.Entity("DeRosaWebApp.Models.Pedido", b =>
                {
                    b.Navigation("ProdutosPedido");

                    b.Navigation("_PedidoDetalhes");
                });
#pragma warning restore 612, 618
        }
    }
}
