using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    public partial class RefactoringDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transport_documents_dictionary_document_type_dictionary_doc~",
                table: "transport_documents");

            migrationBuilder.DropForeignKey(
                name: "FK_transport_documents_vehicles_vehicle_id",
                table: "transport_documents");

            migrationBuilder.DropTable(
                name: "dictionary_document_type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_transport_documents",
                table: "transport_documents");

            migrationBuilder.DropIndex(
                name: "IX_transport_documents_dictionary_document_type_id",
                table: "transport_documents");

            migrationBuilder.DropColumn(
                name: "dictionary_document_type_id",
                table: "transport_documents");

            migrationBuilder.RenameTable(
                name: "transport_documents",
                newName: "insurance_transport_documents");

            migrationBuilder.RenameColumn(
                name: "document_number",
                table: "insurance_transport_documents",
                newName: "policy_number_series_compulsory_сivil_liability_insurance");

            migrationBuilder.RenameIndex(
                name: "IX_transport_documents_vehicle_id",
                table: "insurance_transport_documents",
                newName: "IX_insurance_transport_documents_vehicle_id");

            migrationBuilder.AlterColumn<string>(
                name: "from",
                table: "passes",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "beneficiary",
                table: "insurance_transport_documents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "contract_number_comprehensive_car_insurance",
                table: "insurance_transport_documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "coverage_amount",
                table: "insurance_transport_documents",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "policyholder",
                table: "insurance_transport_documents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "sum_insured",
                table: "insurance_transport_documents",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_insurance_transport_documents",
                table: "insurance_transport_documents",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_insurance_transport_documents_vehicles_vehicle_id",
                table: "insurance_transport_documents",
                column: "vehicle_id",
                principalTable: "vehicles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_insurance_transport_documents_vehicles_vehicle_id",
                table: "insurance_transport_documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_insurance_transport_documents",
                table: "insurance_transport_documents");

            migrationBuilder.DropColumn(
                name: "beneficiary",
                table: "insurance_transport_documents");

            migrationBuilder.DropColumn(
                name: "contract_number_comprehensive_car_insurance",
                table: "insurance_transport_documents");

            migrationBuilder.DropColumn(
                name: "coverage_amount",
                table: "insurance_transport_documents");

            migrationBuilder.DropColumn(
                name: "policyholder",
                table: "insurance_transport_documents");

            migrationBuilder.DropColumn(
                name: "sum_insured",
                table: "insurance_transport_documents");

            migrationBuilder.RenameTable(
                name: "insurance_transport_documents",
                newName: "transport_documents");

            migrationBuilder.RenameColumn(
                name: "policy_number_series_compulsory_сivil_liability_insurance",
                table: "transport_documents",
                newName: "document_number");

            migrationBuilder.RenameIndex(
                name: "IX_insurance_transport_documents_vehicle_id",
                table: "transport_documents",
                newName: "IX_transport_documents_vehicle_id");

            migrationBuilder.AlterColumn<int>(
                name: "from",
                table: "passes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "dictionary_document_type_id",
                table: "transport_documents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_transport_documents",
                table: "transport_documents",
                column: "id");

            migrationBuilder.CreateTable(
                name: "dictionary_document_type",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    document_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictionary_document_type", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transport_documents_dictionary_document_type_id",
                table: "transport_documents",
                column: "dictionary_document_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_transport_documents_dictionary_document_type_dictionary_doc~",
                table: "transport_documents",
                column: "dictionary_document_type_id",
                principalTable: "dictionary_document_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transport_documents_vehicles_vehicle_id",
                table: "transport_documents",
                column: "vehicle_id",
                principalTable: "vehicles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
