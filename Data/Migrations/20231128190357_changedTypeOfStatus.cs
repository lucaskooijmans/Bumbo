using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bumbo.Migrations
{
    /// <inheritdoc />
    public partial class changedTypeOfStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                 name: "Status",
                 table: "Absences",
                 type: "int",
                 nullable: false,
                 oldClrType: typeof(bool),
                 oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Absences",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");      
        }
    }
}
