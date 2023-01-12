using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Migrations
{
    public partial class AddUnion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnionId",
                table: "PromotionPayloads",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Unions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPayloads_UnionId",
                table: "PromotionPayloads",
                column: "UnionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionPayloads_Unions_UnionId",
                table: "PromotionPayloads",
                column: "UnionId",
                principalTable: "Unions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionPayloads_Unions_UnionId",
                table: "PromotionPayloads");

            migrationBuilder.DropTable(
                name: "Unions");

            migrationBuilder.DropIndex(
                name: "IX_PromotionPayloads_UnionId",
                table: "PromotionPayloads");

            migrationBuilder.DropColumn(
                name: "UnionId",
                table: "PromotionPayloads");
        }
    }
}
