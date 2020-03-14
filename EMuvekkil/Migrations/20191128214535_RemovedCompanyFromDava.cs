using Microsoft.EntityFrameworkCore.Migrations;

namespace EMuvekkil.Migrations
{
    public partial class RemovedCompanyFromDava : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
