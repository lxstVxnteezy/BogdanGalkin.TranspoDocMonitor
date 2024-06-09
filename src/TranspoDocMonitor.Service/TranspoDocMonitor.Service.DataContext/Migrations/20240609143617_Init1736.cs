using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    public partial class Init1736 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_insurance_transport_documents_vehicle_id",
                table: "insurance_transport_documents");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_transport_documents_contract_number_comprehensive~",
                table: "insurance_transport_documents",
                column: "contract_number_comprehensive_car_insurance",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_insurance_transport_documents_policy_number_series_compulso~",
                table: "insurance_transport_documents",
                column: "policy_number_series_compulsory_сivil_liability_insurance",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_insurance_transport_documents_vehicle_id",
                table: "insurance_transport_documents",
                column: "vehicle_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_insurance_transport_documents_contract_number_comprehensive~",
                table: "insurance_transport_documents");

            migrationBuilder.DropIndex(
                name: "IX_insurance_transport_documents_policy_number_series_compulso~",
                table: "insurance_transport_documents");

            migrationBuilder.DropIndex(
                name: "IX_insurance_transport_documents_vehicle_id",
                table: "insurance_transport_documents");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_transport_documents_vehicle_id",
                table: "insurance_transport_documents",
                column: "vehicle_id");
        }
    }
}
