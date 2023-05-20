using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buys_AspNetUsers_ApplicationUserId",
                table: "Buys");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_ApplicationUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Drivers_DriverId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImagesId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_WhichCategories_WhichCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ApplicationUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DriverId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImagesId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WhichCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductRegisters_ProductId",
                table: "ProductRegisters");

            migrationBuilder.DropIndex(
                name: "IX_Buys_ApplicationUserId",
                table: "Buys");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImagesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WhichCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Buys");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRegisters_ProductId",
                table: "ProductRegisters",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_ProductRegisters_ProductId",
                table: "ProductRegisters");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductId",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ImagesId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WhichCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Buys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ApplicationUserId",
                table: "Products",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DriverId",
                table: "Products",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImagesId",
                table: "Products",
                column: "ImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_WhichCategoryId",
                table: "Products",
                column: "WhichCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRegisters_ProductId",
                table: "ProductRegisters",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buys_ApplicationUserId",
                table: "Buys",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buys_AspNetUsers_ApplicationUserId",
                table: "Buys",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_ApplicationUserId",
                table: "Products",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Drivers_DriverId",
                table: "Products",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImagesId",
                table: "Products",
                column: "ImagesId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_WhichCategories_WhichCategoryId",
                table: "Products",
                column: "WhichCategoryId",
                principalTable: "WhichCategories",
                principalColumn: "Id");
        }
    }
}
