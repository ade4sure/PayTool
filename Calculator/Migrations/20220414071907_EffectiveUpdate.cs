using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Migrations
{
    public partial class EffectiveUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "EffectiveDates",
                newName: "OperationStartDate");

            migrationBuilder.RenameColumn(
                name: "OperationalDate",
                table: "EffectiveDates",
                newName: "OperationEndDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "EffectiveDates",
                newName: "ApprovedStartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedEndDate",
                table: "EffectiveDates",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedEndDate",
                table: "EffectiveDates");

            migrationBuilder.RenameColumn(
                name: "OperationStartDate",
                table: "EffectiveDates",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "OperationEndDate",
                table: "EffectiveDates",
                newName: "OperationalDate");

            migrationBuilder.RenameColumn(
                name: "ApprovedStartDate",
                table: "EffectiveDates",
                newName: "EndDate");
        }
    }
}
