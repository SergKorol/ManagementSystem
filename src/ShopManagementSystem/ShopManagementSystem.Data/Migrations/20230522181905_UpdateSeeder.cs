using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_Products_ProductId",
                table: "ShopProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_Shops_ShopId",
                table: "ShopProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopProduct",
                table: "ShopProduct");

            migrationBuilder.RenameTable(
                name: "ShopProduct",
                newName: "ShopProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ShopProduct_ProductId",
                table: "ShopProducts",
                newName: "IX_ShopProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopProducts",
                table: "ShopProducts",
                columns: new[] { "ShopId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProducts_Products_ProductId",
                table: "ShopProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProducts_Shops_ShopId",
                table: "ShopProducts",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "ShopId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProducts_Products_ProductId",
                table: "ShopProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopProducts_Shops_ShopId",
                table: "ShopProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopProducts",
                table: "ShopProducts");

            migrationBuilder.RenameTable(
                name: "ShopProducts",
                newName: "ShopProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ShopProducts_ProductId",
                table: "ShopProduct",
                newName: "IX_ShopProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopProduct",
                table: "ShopProduct",
                columns: new[] { "ShopId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_Products_ProductId",
                table: "ShopProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_Shops_ShopId",
                table: "ShopProduct",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "ShopId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
