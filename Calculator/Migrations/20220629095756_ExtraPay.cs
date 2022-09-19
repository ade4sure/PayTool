using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Migrations
{
    public partial class ExtraPay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CallDuty",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProffesionalAllowance",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ShiftDuty",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallDuty",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "ProffesionalAllowance",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "ShiftDuty",
                table: "Steps");
        }
    }
}
