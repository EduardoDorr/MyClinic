using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyClinic.Procedures.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMinimumSchedulingNoticeToProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                schema: "Procedure",
                table: "Procedures",
                type: "numeric(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "MinimumSchedulingNotice",
                schema: "Procedure",
                table: "Procedures",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumSchedulingNotice",
                schema: "Procedure",
                table: "Procedures");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                schema: "Procedure",
                table: "Procedures",
                type: "numeric(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,2)");
        }
    }
}
