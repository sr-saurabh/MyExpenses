using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyExpenses.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedfromUserRelationInConact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FromUserId",
                table: "Contacts",
                column: "FromUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AppUsers_FromUserId",
                table: "Contacts",
                column: "FromUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AppUsers_FromUserId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_FromUserId",
                table: "Contacts");
        }
    }
}
