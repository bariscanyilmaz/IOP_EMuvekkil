using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMuvekkil.Migrations
{
    public partial class Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DavaStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DavaStates", x => x.Id);
                });

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

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "DavaStates");

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

            migrationBuilder.AddColumn<int>(
                name: "DavaId",
                table: "Davas",
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
    }
}
