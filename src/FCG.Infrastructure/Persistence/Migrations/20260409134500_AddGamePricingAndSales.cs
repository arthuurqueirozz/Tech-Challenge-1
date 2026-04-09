using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGamePricingAndSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Games_GameId",
                table: "Promotions");

            migrationBuilder.RenameTable(
                name: "Promotions",
                newName: "Sales");

            migrationBuilder.RenameIndex(
                name: "IX_Promotions_GameId",
                table: "Sales",
                newName: "IX_Sales_GameId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Games",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.Sql("DELETE FROM [Sales] WHERE [GameId] IS NULL;");

            migrationBuilder.AlterColumn<Guid>(
                name: "GameId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Games_GameId",
                table: "Sales",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Games_GameId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Sales",
                newName: "Promotions");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_GameId",
                table: "Promotions",
                newName: "IX_Promotions_GameId");

            migrationBuilder.AlterColumn<Guid>(
                name: "GameId",
                table: "Promotions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Games_GameId",
                table: "Promotions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
