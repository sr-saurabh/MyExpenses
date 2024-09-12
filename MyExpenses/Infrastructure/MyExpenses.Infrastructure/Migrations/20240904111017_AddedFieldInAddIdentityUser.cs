using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyExpenses.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldInAddIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Settlements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsGoogleLoggedIn",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Settlements");

            migrationBuilder.DropColumn(
                name: "IsGoogleLoggedIn",
                table: "AspNetUsers");
        }
    }
}
