using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerReview.Migrations
{
    public partial class NewClassName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Beers_BeerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Drinkers_DrinkerId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "BeerDrinker");

            migrationBuilder.DropTable(
                name: "Drinkers");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BeerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_DrinkerId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "BeerId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "DrinkerId",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BeerRatings",
                columns: table => new
                {
                    BeerRatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BeerId = table.Column<int>(type: "int", nullable: false),
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerRatings", x => x.BeerRatingId);
                    table.ForeignKey(
                        name: "FK_BeerRatings_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "BeerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerRatings_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "ReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerRatings_BeerId",
                table: "BeerRatings",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerRatings_ReviewId",
                table: "BeerRatings",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "BeerRatings");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "BeerId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DrinkerId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Drinkers",
                columns: table => new
                {
                    DrinkerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SignUp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinkers", x => x.DrinkerId);
                });

            migrationBuilder.CreateTable(
                name: "BeerDrinker",
                columns: table => new
                {
                    BeerDrinkerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BeerId = table.Column<int>(type: "int", nullable: false),
                    DrinkerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerDrinker", x => x.BeerDrinkerId);
                    table.ForeignKey(
                        name: "FK_BeerDrinker_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "BeerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerDrinker_Drinkers_DrinkerId",
                        column: x => x.DrinkerId,
                        principalTable: "Drinkers",
                        principalColumn: "DrinkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BeerId",
                table: "Reviews",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DrinkerId",
                table: "Reviews",
                column: "DrinkerId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerDrinker_BeerId",
                table: "BeerDrinker",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerDrinker_DrinkerId",
                table: "BeerDrinker",
                column: "DrinkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Beers_BeerId",
                table: "Reviews",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "BeerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Drinkers_DrinkerId",
                table: "Reviews",
                column: "DrinkerId",
                principalTable: "Drinkers",
                principalColumn: "DrinkerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
