using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Migrations
{
    public partial class StructureCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentStructures_PayCategorys_PayCategoryId",
                table: "PaymentStructures");

            migrationBuilder.DropIndex(
                name: "IX_PaymentStructures_PayCategoryId",
                table: "PaymentStructures");

            migrationBuilder.DropColumn(
                name: "PayCategoryId",
                table: "PaymentStructures");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PaymentStructures",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentStructures_CategoryId",
                table: "PaymentStructures",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentStructures_PayCategorys_CategoryId",
                table: "PaymentStructures",
                column: "CategoryId",
                principalTable: "PayCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentStructures_PayCategorys_CategoryId",
                table: "PaymentStructures");

            migrationBuilder.DropIndex(
                name: "IX_PaymentStructures_CategoryId",
                table: "PaymentStructures");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PaymentStructures");

            migrationBuilder.AddColumn<int>(
                name: "PayCategoryId",
                table: "PaymentStructures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentStructures_PayCategoryId",
                table: "PaymentStructures",
                column: "PayCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentStructures_PayCategorys_PayCategoryId",
                table: "PaymentStructures",
                column: "PayCategoryId",
                principalTable: "PayCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
