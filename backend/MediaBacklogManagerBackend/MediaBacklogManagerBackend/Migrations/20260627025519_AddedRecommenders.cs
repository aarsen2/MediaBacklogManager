using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaBacklogManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecommenders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
