using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartInvestor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    DividendGoalPerYear = table.Column<decimal>(type: "numeric", nullable: false),
                    AmmountInvested = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentAmmountInvested = table.Column<decimal>(type: "numeric", nullable: false),
                    Appreciation = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockPlanning",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ticket = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentQuantity = table.Column<decimal>(type: "numeric", nullable: false),
                    RequiredQuantity = table.Column<decimal>(type: "numeric", nullable: false),
                    CeilingPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmmountInvested = table.Column<decimal>(type: "numeric", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentTotalAmmountInvested = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectedYield = table.Column<decimal>(type: "numeric", nullable: false),
                    MinimumYieldRequired = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentYield = table.Column<decimal>(type: "numeric", nullable: false),
                    Buy = table.Column<bool>(type: "boolean", nullable: false),
                    Goal = table.Column<decimal>(type: "numeric", nullable: false),
                    LogoUrl = table.Column<string>(type: "text", nullable: true),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPlanning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockPlanning_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockPlanning_WalletId",
                table: "StockPlanning",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockPlanning");

            migrationBuilder.DropTable(
                name: "Wallet");
        }
    }
}
