using Microsoft.EntityFrameworkCore.Migrations;

namespace EMuvekkil.Migrations
{
    public partial class CompanyAddedMuvekkil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Davas_Companies_CompanyId",
                table: "Davas");

            migrationBuilder.DropIndex(
                name: "IX_Davas_CompanyId",
                table: "Davas");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Davas");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Davas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Davas_CompanyId",
                table: "Davas",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Davas_Companies_CompanyId",
                table: "Davas",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
