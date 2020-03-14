using Microsoft.EntityFrameworkCore.Migrations;

namespace EMuvekkil.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DavaId",
                table: "Davas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSurname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Davas_DavaId",
                table: "Davas",
                column: "DavaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Davas_Davas_DavaId",
                table: "Davas",
                column: "DavaId",
                principalTable: "Davas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Davas_Davas_DavaId",
                table: "Davas");

            migrationBuilder.DropIndex(
                name: "IX_Davas_DavaId",
                table: "Davas");

            migrationBuilder.DropColumn(
                name: "DavaId",
                table: "Davas");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NameSurname",
                table: "AspNetUsers");
        }
    }
}
