using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resenhando2.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImplementingCoverImageReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Review",
                type: "NVARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Review");
        }
    }
}
