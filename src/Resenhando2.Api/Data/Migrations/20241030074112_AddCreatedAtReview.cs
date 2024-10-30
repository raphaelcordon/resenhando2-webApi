using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resenhando2.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Review",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Review",
                type: "DATETIMEOFFSET",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Review");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Review",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER");
        }
    }
}
