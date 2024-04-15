using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    public partial class AddEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentNumber = table.Column<int>(type: "integer", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpirationDateOfIssue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserTransportDocId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsertTransportId = table.Column<Guid>(type: "uuid", nullable: false),
                    DictionaryDocumentTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportDocuments_DocumentTypes_DictionaryDocumentTypeId",
                        column: x => x.DictionaryDocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportDocuments_UsersTransports_UsertTransportId",
                        column: x => x.UsertTransportId,
                        principalTable: "UsersTransports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportDocuments_DictionaryDocumentTypeId",
                table: "TransportDocuments",
                column: "DictionaryDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportDocuments_UsertTransportId",
                table: "TransportDocuments",
                column: "UsertTransportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportDocuments");

            migrationBuilder.DropTable(
                name: "DocumentTypes");
        }
    }
}
