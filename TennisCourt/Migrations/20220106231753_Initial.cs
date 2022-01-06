using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TennisCourt.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    CourtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Location = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SurfaceType = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Public = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Private = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.CourtId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Location = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "CourtPlayer",
                columns: table => new
                {
                    CourtPlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtPlayer", x => x.CourtPlayerId);
                    table.ForeignKey(
                        name: "FK_CourtPlayer_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "CourtId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourtPlayer_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourtPlayer_CourtId",
                table: "CourtPlayer",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtPlayer_PlayerId",
                table: "CourtPlayer",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourtPlayer");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
