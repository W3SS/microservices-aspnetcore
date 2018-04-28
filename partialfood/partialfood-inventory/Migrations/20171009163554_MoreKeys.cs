using Microsoft.EntityFrameworkCore.Migrations;

namespace PartialFoods.Services.InventoryServer.Migrations
{
    public partial class MoreKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Activities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Activities",
                type: "int4",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Activities");
        }
    }
}
