using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaBacklogManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecommenderNavigationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommenders_UserMedia_UserMediaId",
                table: "Recommenders");

            migrationBuilder.DropIndex(
                name: "IX_Recommenders_UserMediaId",
                table: "Recommenders");

            migrationBuilder.DropColumn(
                name: "UserMediaId",
                table: "Recommenders");

            migrationBuilder.CreateTable(
                name: "RecommenderUserMedia",
                columns: table => new
                {
                    RecommendersId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserMediaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommenderUserMedia", x => new { x.RecommendersId, x.UserMediaId });
                    table.ForeignKey(
                        name: "FK_RecommenderUserMedia_Recommenders_RecommendersId",
                        column: x => x.RecommendersId,
                        principalTable: "Recommenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecommenderUserMedia_UserMedia_UserMediaId",
                        column: x => x.UserMediaId,
                        principalTable: "UserMedia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecommenderUserMedia_UserMediaId",
                table: "RecommenderUserMedia",
                column: "UserMediaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecommenderUserMedia");

            migrationBuilder.AddColumn<int>(
                name: "UserMediaId",
                table: "Recommenders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recommenders_UserMediaId",
                table: "Recommenders",
                column: "UserMediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommenders_UserMedia_UserMediaId",
                table: "Recommenders",
                column: "UserMediaId",
                principalTable: "UserMedia",
                principalColumn: "Id");
        }
    }
}
