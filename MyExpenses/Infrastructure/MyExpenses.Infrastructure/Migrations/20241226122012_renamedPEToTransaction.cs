using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyExpenses.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renamedPEToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalExpenses_Accounts_AccountId",
                table: "PersonalExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalExpenses_AppUsers_AppUserId",
                table: "PersonalExpenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalExpenses",
                table: "PersonalExpenses");

            migrationBuilder.RenameTable(
                name: "PersonalExpenses",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalExpenses_AppUserId",
                table: "Transactions",
                newName: "IX_Transactions_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalExpenses_AccountId",
                table: "Transactions",
                newName: "IX_Transactions_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AppUsers_AppUserId",
                table: "Transactions",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AppUsers_AppUserId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "PersonalExpenses");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AppUserId",
                table: "PersonalExpenses",
                newName: "IX_PersonalExpenses_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AccountId",
                table: "PersonalExpenses",
                newName: "IX_PersonalExpenses_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalExpenses",
                table: "PersonalExpenses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalExpenses_Accounts_AccountId",
                table: "PersonalExpenses",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalExpenses_AppUsers_AppUserId",
                table: "PersonalExpenses",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
