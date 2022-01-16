using Microsoft.EntityFrameworkCore.Migrations;

namespace Proje.Data.Migrations
{
    public partial class AddDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountRate",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "PromotionalPrice",
                table: "Menu");

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodId = table.Column<int>(type: "int", nullable: true),
                    DiscountRate = table.Column<double>(type: "float", nullable: false),
                    PromotionalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Discount_Menu_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Menu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discount_FoodId",
                table: "Discount",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.AddColumn<double>(
                name: "DiscountRate",
                table: "Menu",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PromotionalPrice",
                table: "Menu",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
