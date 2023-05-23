using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NyWine.Migrations
{
    /// <inheritdoc />
    public partial class removedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WineDescription_Category_CategoryId",
                table: "WineDescription");

            migrationBuilder.DropIndex(
                name: "IX_WineDescription_CategoryId",
                table: "WineDescription");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "WineDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "WineDescription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WineDescription_CategoryId",
                table: "WineDescription",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WineDescription_Category_CategoryId",
                table: "WineDescription",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
