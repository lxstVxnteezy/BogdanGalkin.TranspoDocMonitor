using System;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DictionaryDocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryDocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    sur_name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    hash = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    make = table.Column<string>(type: "text", nullable: false),
                    model = table.Column<string>(type: "text", nullable: false),
                    year_of_issue = table.Column<string>(type: "text", nullable: false),
                    auto_color = table.Column<string>(type: "text", nullable: false),
                    registration_number = table.Column<string>(type: "text", nullable: false),
                    vehicle_identification_number = table.Column<string>(type: "text", nullable: false),
                    engine_capacity = table.Column<double>(type: "double precision", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    VehicleDiagnosticReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicles_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "passes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    expiration_date_of_issue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PassNumber = table.Column<int>(type: "integer", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passes", x => x.id);
                    table.ForeignKey(
                        name: "FK_passes_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transport_documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    document_number = table.Column<int>(type: "integer", nullable: false),
                    data_of_issue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiration_date_of_Issue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dictionary_document_type_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_transport_documents_DictionaryDocumentTypes_dictionary_docu~",
                        column: x => x.dictionary_document_type_id,
                        principalTable: "DictionaryDocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transport_documents_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_diagnostic_report",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    diagnostic_card_number = table.Column<BigInteger>(type: "numeric", nullable: false),
                    expiration_date_of_issue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_diagnostic_report", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicle_diagnostic_report_vehicles_id",
                        column: x => x.id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_passes_vehicle_id",
                table: "passes",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_transport_documents_dictionary_document_type_id",
                table: "transport_documents",
                column: "dictionary_document_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_transport_documents_vehicle_id",
                table: "transport_documents",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_UserId",
                table: "vehicles",
                column: "UserId");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("53361d0b-8b55-46bd-b097-ca36972d82ff"), "administrator" },
                    { new Guid("d813a26d-dc9a-4664-8cb7-b2280a73a727"), "member" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "login", "first_name", "last_name", "sur_name", "Email", "hash", "role_id" },
                values: new object[,]
                {
                    { new Guid("4b88dd01-0f6b-42cf-b5ea-1fbf072b4283"), "admin", "admin", "admin", "admin", "admin@example.com", "ECE54AF8D883D1B7A7062A475617B8B2", new Guid("53361d0b-8b55-46bd-b097-ca36972d82ff") }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "passes");

            migrationBuilder.DropTable(
                name: "transport_documents");

            migrationBuilder.DropTable(
                name: "vehicle_diagnostic_report");

            migrationBuilder.DropTable(
                name: "DictionaryDocumentTypes");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");


            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "name",
                keyValues: new object[] { "administrator", "member" });
        }
    }
}
