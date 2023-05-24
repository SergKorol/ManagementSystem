using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductShop");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Shops",
                newName: "ShopId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.CreateTable(
                name: "ShopProduct",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopProduct", x => new { x.ShopId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ShopProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopProduct_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopProduct_ProductId",
                table: "ShopProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopProduct");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "Shops",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "ProductShop",
                columns: table => new
                {
                    ProductsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ShopsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShop", x => new { x.ProductsId, x.ShopsId });
                    table.ForeignKey(
                        name: "FK_ProductShop_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductShop_Shops_ShopsId",
                        column: x => x.ShopsId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductShop_ShopsId",
                table: "ProductShop",
                column: "ShopsId");
        }
    }
}
