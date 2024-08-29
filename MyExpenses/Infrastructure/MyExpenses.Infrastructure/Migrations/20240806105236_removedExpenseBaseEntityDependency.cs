using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyExpenses.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removedExpenseBaseEntityDependency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseBaseEntity_AppUsers_FromUserId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseBaseEntity_AppUsers_ToUserId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseBaseEntity",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ExpenseBaseEntity");

            migrationBuilder.RenameTable(
                name: "ExpenseBaseEntity",
                newName: "UserExpenses");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseBaseEntity_ToUserId",
                table: "UserExpenses",
                newName: "IX_UserExpenses_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseBaseEntity_FromUserId",
                table: "UserExpenses",
                newName: "IX_UserExpenses_FromUserId");

            migrationBuilder.AlterColumn<int>(
                name: "ToUserId",
                table: "UserExpenses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FromUserId",
                table: "UserExpenses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExpenses",
                table: "UserExpenses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExpenses_AppUsers_FromUserId",
                table: "UserExpenses",
                column: "FromUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExpenses_AppUsers_ToUserId",
                table: "UserExpenses",
                column: "ToUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserExpenses_AppUsers_FromUserId",
                table: "UserExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExpenses_AppUsers_ToUserId",
                table: "UserExpenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExpenses",
                table: "UserExpenses");

            migrationBuilder.RenameTable(
                name: "UserExpenses",
                newName: "ExpenseBaseEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserExpenses_ToUserId",
                table: "ExpenseBaseEntity",
                newName: "IX_ExpenseBaseEntity_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserExpenses_FromUserId",
                table: "ExpenseBaseEntity",
                newName: "IX_ExpenseBaseEntity_FromUserId");

            migrationBuilder.AlterColumn<int>(
                name: "ToUserId",
                table: "ExpenseBaseEntity",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "FromUserId",
                table: "ExpenseBaseEntity",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ExpenseBaseEntity",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseBaseEntity",
                table: "ExpenseBaseEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseBaseEntity_AppUsers_FromUserId",
                table: "ExpenseBaseEntity",
                column: "FromUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseBaseEntity_AppUsers_ToUserId",
                table: "ExpenseBaseEntity",
                column: "ToUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
