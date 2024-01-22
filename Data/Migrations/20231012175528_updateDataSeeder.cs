using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bumbo.Migrations
{
    /// <inheritdoc />
    public partial class updateDataSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Norms",
                table: "Norms");
            
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Norms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Norms",
                table: "Norms",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Norms",
                table: "Norms");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Norms",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Norms",
                table: "Norms",
                column: "Id");
            
        }
    }
}
