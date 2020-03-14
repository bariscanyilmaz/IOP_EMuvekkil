using Microsoft.EntityFrameworkCore.Migrations;

namespace EMuvekkil.Migrations
{
    public partial class AddedMesageMasrafIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Messages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Documents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Documents");
        }
    }
}
