using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class AddCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pizze",
                table: "Pizze");

            migrationBuilder.RenameTable(
                name: "Pizze",
                newName: "pizze");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "pizze",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "pizze",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "pizze",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_pizze",
                table: "pizze",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pizze_CategoryId",
                table: "pizze",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_pizze_category_CategoryId",
                table: "pizze",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pizze_category_CategoryId",
                table: "pizze");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pizze",
                table: "pizze");

            migrationBuilder.DropIndex(
                name: "IX_pizze_CategoryId",
                table: "pizze");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "pizze");

            migrationBuilder.RenameTable(
                name: "pizze",
                newName: "Pizze");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pizze",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "Pizze",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pizze",
                table: "Pizze",
                column: "Id");
        }
    }
}
