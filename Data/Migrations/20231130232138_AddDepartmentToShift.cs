using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bumbo.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentToShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentName",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_DepartmentName",
                table: "Shifts",
                column: "DepartmentName");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Departments_DepartmentName",
                table: "Shifts",
                column: "DepartmentName",
                principalTable: "Departments",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Departments_DepartmentName",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_DepartmentName",
                table: "Shifts");
        }
    }
}
