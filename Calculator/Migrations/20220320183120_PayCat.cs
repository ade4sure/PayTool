using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Migrations
{
    public partial class PayCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayCategoryId",
                table: "PaymentStructures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PayCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayCategorys", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentStructures_PayCategorys_PayCategoryId",
                table: "PaymentStructures");

            migrationBuilder.DropTable(
                name: "PayCategorys");

            migrationBuilder.DropIndex(
                name: "IX_PaymentStructures_PayCategoryId",
                table: "PaymentStructures");

            migrationBuilder.DropColumn(
                name: "PayCategoryId",
                table: "PaymentStructures");
        }
    }
}
