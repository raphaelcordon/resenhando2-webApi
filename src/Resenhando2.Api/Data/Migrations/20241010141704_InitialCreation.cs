using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resenhando2.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewType = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: false),
                    SpotifyId = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: false),
                    ReviewText = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    ReviewBody = table.Column<string>(type: "NVARCHAR(MAX)", maxLength: 10000, nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
