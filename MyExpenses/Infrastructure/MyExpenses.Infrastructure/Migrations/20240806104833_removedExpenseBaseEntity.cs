using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyExpenses.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removedExpenseBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseBaseEntity_AppUsers_AppUserId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseBaseEntity_AppUsers_PayerId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseBaseEntity_UserGroups_GroupId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupExpenseShares_ExpenseBaseEntity_GroupExpenseId",
                table: "GroupExpenseShares");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseBaseEntity_AppUserId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseBaseEntity_GroupId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseBaseEntity_PayerId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropColumn(
                name: "PayerId",
                table: "ExpenseBaseEntity");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ExpenseBaseEntity");

            migrationBuilder.CreateTable(
                name: "GroupExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    PayerId = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ActivityStatusChangedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActivityStatusChangedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ActivityStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupExpenses_AppUsers_PayerId",
                        column: x => x.PayerId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupExpenses_UserGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ActivityStatusChangedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActivityStatusChangedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ActivityStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalExpenses_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupExpenses_GroupId",
                table: "GroupExpenses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupExpenses_PayerId",
                table: "GroupExpenses",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalExpenses_AppUserId",
                table: "PersonalExpenses",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupExpenseShares_GroupExpenses_GroupExpenseId",
                table: "GroupExpenseShares",
                column: "GroupExpenseId",
                principalTable: "GroupExpenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupExpenseShares_GroupExpenses_GroupExpenseId",
                table: "GroupExpenseShares");

            migrationBuilder.DropTable(
                name: "GroupExpenses");

            migrationBuilder.DropTable(
                name: "PersonalExpenses");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "ExpenseBaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "ExpenseBaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayerId",
                table: "ExpenseBaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ExpenseBaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBaseEntity_AppUserId",
                table: "ExpenseBaseEntity",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBaseEntity_GroupId",
                table: "ExpenseBaseEntity",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBaseEntity_PayerId",
                table: "ExpenseBaseEntity",
                column: "PayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseBaseEntity_AppUsers_AppUserId",
                table: "ExpenseBaseEntity",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseBaseEntity_AppUsers_PayerId",
                table: "ExpenseBaseEntity",
                column: "PayerId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseBaseEntity_UserGroups_GroupId",
                table: "ExpenseBaseEntity",
                column: "GroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupExpenseShares_ExpenseBaseEntity_GroupExpenseId",
                table: "GroupExpenseShares",
                column: "GroupExpenseId",
                principalTable: "ExpenseBaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
