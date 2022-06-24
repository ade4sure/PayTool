using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Migrations
{
    public partial class Pstructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveDates_CStructures_CStructureId",
                table: "EffectiveDates");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_CStructures_CStructureId",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "CStructures");

            migrationBuilder.CreateTable(
                name: "PaymentStructures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStructures", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveDates_PaymentStructures_CStructureId",
                table: "EffectiveDates",
                column: "CStructureId",
                principalTable: "PaymentStructures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_PaymentStructures_CStructureId",
                table: "Grades",
                column: "CStructureId",
                principalTable: "PaymentStructures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveDates_PaymentStructures_CStructureId",
                table: "EffectiveDates");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_PaymentStructures_CStructureId",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "PaymentStructures");

            migrationBuilder.CreateTable(
                name: "CStructures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CStructures", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveDates_CStructures_CStructureId",
                table: "EffectiveDates",
                column: "CStructureId",
                principalTable: "CStructures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_CStructures_CStructureId",
                table: "Grades",
                column: "CStructureId",
                principalTable: "CStructures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
