using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaBacklogManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddContentRatingToShows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Show_ContentRating",
                table: "Media",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Show_ContentRating",
                table: "Media");
        }
    }
}
