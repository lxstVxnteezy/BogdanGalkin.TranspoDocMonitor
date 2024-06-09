using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    public partial class Init1637 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_of_issue",
                table: "insurance_transport_documents");

            migrationBuilder.DropColumn(
                name: "expiration_date_of_Issue",
                table: "insurance_transport_documents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "data_of_issue",
                table: "insurance_transport_documents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "expiration_date_of_Issue",
                table: "insurance_transport_documents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
