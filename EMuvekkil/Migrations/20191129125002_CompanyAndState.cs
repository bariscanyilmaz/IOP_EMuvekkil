using Microsoft.EntityFrameworkCore.Migrations;

namespace EMuvekkil.Migrations
{
    public partial class CompanyAndState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Davas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DavaStateId",
                table: "Davas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Davas_CompanyId",
                table: "Davas",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Davas_DavaStateId",
                table: "Davas",
                column: "DavaStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Davas_Companies_CompanyId",
                table: "Davas",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Davas_DavaStates_DavaStateId",
                table: "Davas",
                column: "DavaStateId",
                principalTable: "DavaStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Davas_Companies_CompanyId",
                table: "Davas");

            migrationBuilder.DropForeignKey(
                name: "FK_Davas_DavaStates_DavaStateId",
                table: "Davas");

            migrationBuilder.DropIndex(
                name: "IX_Davas_CompanyId",
                table: "Davas");

            migrationBuilder.DropIndex(
                name: "IX_Davas_DavaStateId",
                table: "Davas");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Davas");

            migrationBuilder.DropColumn(
                name: "DavaStateId",
                table: "Davas");
        }
    }
}
