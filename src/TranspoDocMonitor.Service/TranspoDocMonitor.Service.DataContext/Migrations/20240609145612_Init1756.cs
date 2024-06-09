using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    public partial class Init1756 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "horsepower",
                table: "vehicles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "horsepower",
                table: "vehicles");
        }
    }
}
