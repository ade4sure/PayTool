using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Migrations
{
    public partial class AditiondalExtras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShiftDuty",
                table: "Steps",
                newName: "ShiftDutyOthers");

            migrationBuilder.RenameColumn(
                name: "CallDuty",
                table: "Steps",
                newName: "ShiftDutyNurses");

            migrationBuilder.AddColumn<decimal>(
                name: "CallDutyASUU",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CallDutyNurses",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CallDutyOthers",
                table: "Steps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallDutyASUU",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "CallDutyNurses",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "CallDutyOthers",
                table: "Steps");

            migrationBuilder.RenameColumn(
                name: "ShiftDutyOthers",
                table: "Steps",
                newName: "ShiftDuty");

            migrationBuilder.RenameColumn(
                name: "ShiftDutyNurses",
                table: "Steps",
                newName: "CallDuty");
        }
    }
}
