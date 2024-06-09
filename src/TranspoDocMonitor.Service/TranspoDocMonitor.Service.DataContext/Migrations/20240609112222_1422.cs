using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    public partial class _1422 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_vehicle_diagnostic_report_diagnostic_card_number",
                table: "vehicle_diagnostic_report",
                column: "diagnostic_card_number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_vehicle_diagnostic_report_diagnostic_card_number",
                table: "vehicle_diagnostic_report");
        }
    }
}
