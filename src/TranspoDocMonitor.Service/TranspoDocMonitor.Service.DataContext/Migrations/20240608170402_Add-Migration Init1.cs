using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    public partial class AddMigrationInit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_vehicle_diagnostic_report_vehicle_diagnostic_repor~",
                table: "vehicles");

            migrationBuilder.DropIndex(
                name: "IX_vehicles_vehicle_diagnostic_report_id",
                table: "vehicles");

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleId",
                table: "vehicle_diagnostic_report",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_diagnostic_report_VehicleId",
                table: "vehicle_diagnostic_report",
                column: "VehicleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_diagnostic_report_vehicles_VehicleId",
                table: "vehicle_diagnostic_report",
                column: "VehicleId",
                principalTable: "vehicles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_diagnostic_report_vehicles_VehicleId",
                table: "vehicle_diagnostic_report");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_diagnostic_report_VehicleId",
                table: "vehicle_diagnostic_report");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "vehicle_diagnostic_report");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_vehicle_diagnostic_report_id",
                table: "vehicles",
                column: "vehicle_diagnostic_report_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_vehicle_diagnostic_report_vehicle_diagnostic_repor~",
                table: "vehicles",
                column: "vehicle_diagnostic_report_id",
                principalTable: "vehicle_diagnostic_report",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
