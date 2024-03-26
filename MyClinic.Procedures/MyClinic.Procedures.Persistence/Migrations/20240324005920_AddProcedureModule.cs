using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyClinic.Procedures.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProcedureModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Procedure");

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "Procedure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Error = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Procedures",
                schema: "Procedure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    SpecialityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    Month = table.Column<byte>(type: "tinyint", nullable: false),
                    Day = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_Active",
                schema: "Procedure",
                table: "Procedures",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_Name",
                schema: "Procedure",
                table: "Procedures",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_SpecialityId",
                schema: "Procedure",
                table: "Procedures",
                column: "SpecialityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "Procedure");

            migrationBuilder.DropTable(
                name: "Procedures",
                schema: "Procedure");
        }
    }
}
