using Microsoft.EntityFrameworkCore.Migrations;

namespace EMuvekkil.Migrations
{
    public partial class RemovedDavaState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Davas_DavaStates_DavaStateId",
                table: "Davas");

            migrationBuilder.DropIndex(
                name: "IX_Davas_DavaStateId",
                table: "Davas");

            migrationBuilder.DropColumn(
                name: "DavaStateId",
                table: "Davas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DavaStateId",
                table: "Davas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Davas_DavaStateId",
                table: "Davas",
                column: "DavaStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Davas_DavaStates_DavaStateId",
                table: "Davas",
                column: "DavaStateId",
                principalTable: "DavaStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
