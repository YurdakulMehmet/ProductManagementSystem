using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class DbUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productPhotos_Products_ProductId",
                table: "productPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productPhotos",
                table: "productPhotos");

            migrationBuilder.DropColumn(
                name: "ChildCategoryId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "productPhotos",
                newName: "ProductPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_productPhotos_ProductId",
                table: "ProductPhotos",
                newName: "IX_ProductPhotos_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPhotos",
                table: "ProductPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPhotos",
                table: "ProductPhotos");

            migrationBuilder.RenameTable(
                name: "ProductPhotos",
                newName: "productPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPhotos_ProductId",
                table: "productPhotos",
                newName: "IX_productPhotos_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "ChildCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_productPhotos",
                table: "productPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_productPhotos_Products_ProductId",
                table: "productPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
