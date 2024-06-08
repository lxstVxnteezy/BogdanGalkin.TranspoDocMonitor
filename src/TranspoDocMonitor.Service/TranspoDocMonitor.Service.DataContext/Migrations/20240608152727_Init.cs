using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "vehicle_diagnostic_report",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    diagnostic_card_number = table.Column<long>(type: "bigint", nullable: false),
                    expiration_date_of_issue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_diagnostic_report", x => x.id);
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
                    vehicle_diagnostic_report_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vehicles_vehicle_diagnostic_report_vehicle_diagnostic_repor~",
                        column: x => x.vehicle_diagnostic_report_id,
                        principalTable: "vehicle_diagnostic_report",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "insurance_transport_documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_of_issue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiration_date_of_Issue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    policyholder = table.Column<string>(type: "text", nullable: false),
                    beneficiary = table.Column<string>(type: "text", nullable: false),
                    contract_number_comprehensive_car_insurance = table.Column<int>(type: "integer", nullable: false),
                    policy_number_series_compulsory_сivil_liability_insurance = table.Column<int>(type: "integer", nullable: false),
                    sum_insured = table.Column<decimal>(type: "numeric", nullable: false),
                    coverage_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_transport_documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_insurance_transport_documents_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
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
                    from = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_insurance_transport_documents_vehicle_id",
                table: "insurance_transport_documents",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_passes_vehicle_id",
                table: "passes",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_user_id",
                table: "vehicles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_vehicle_diagnostic_report_id",
                table: "vehicles",
                column: "vehicle_diagnostic_report_id",
                unique: true);

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
                name: "insurance_transport_documents");

            migrationBuilder.DropTable(
                name: "passes");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "vehicle_diagnostic_report");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
