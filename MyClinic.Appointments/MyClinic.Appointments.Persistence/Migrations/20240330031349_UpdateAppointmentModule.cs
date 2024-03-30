using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyClinic.Appointments.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CalenderEventId",
                schema: "Appointment",
                table: "Appointments",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                schema: "Appointment",
                table: "Appointments",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalenderEventId",
                schema: "Appointment",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Cost",
                schema: "Appointment",
                table: "Appointments");
        }
    }
}
