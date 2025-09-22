using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Triguinho.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Rules = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SequenceNumber = table.Column<int>(type: "INT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    EndDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Status = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    ResultDrawnAmount = table.Column<int>(type: "int", nullable: true),
                    ResultDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ResultGenerationMoment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GameId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rounds_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositValue = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    Multiplier = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrizeValue = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    BetDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    GuessValueChosen = table.Column<int>(type: "int", nullable: false),
                    GuessTypeBet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoundId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Bets_BetDate",
                table: "Bets",
                column: "BetDate");

            migrationBuilder.CreateIndex(
                name: "IDX_Bets_BetId",
                table: "Bets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IDX_Bets_RoundId",
                table: "Bets",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IDX_Bets_Status",
                table: "Bets",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IDX_Game_Id",
                table: "Games",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IDX_Game_IsActive",
                table: "Games",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IDX_Game_Name",
                table: "Games",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IDX_Round_GameId",
                table: "Rounds",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IDX_Round_Status",
                table: "Rounds",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_GameId_SequenceNumber",
                table: "Rounds",
                columns: new[] { "GameId", "SequenceNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
