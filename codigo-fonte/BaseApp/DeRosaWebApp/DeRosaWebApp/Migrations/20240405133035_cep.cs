using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeRosaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class cep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "_Email",
                table: "Clientes",
                newName: "Logradouro");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Clientes",
                newName: "UF");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "Clientes",
                newName: "Complemento");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Clientes",
                newName: "Cidade");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Clientes",
                newName: "Bairro");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clientes",
                newName: "Numero");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Clientes",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "UF",
                table: "Clientes",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Clientes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Clientes",
                newName: "_Email");

            migrationBuilder.RenameColumn(
                name: "Complemento",
                table: "Clientes",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Clientes",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Clientes",
                newName: "PasswordHash");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Clientes",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Clientes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
