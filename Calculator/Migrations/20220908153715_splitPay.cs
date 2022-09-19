using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Migrations
{
    public partial class splitPay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Contiss",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ContissRent",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Conuss",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Hazard",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contiss",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "ContissRent",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "Conuss",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "Hazard",
                table: "Steps");
        }
    }
}
