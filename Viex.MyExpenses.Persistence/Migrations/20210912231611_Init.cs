using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Viex.MyExpenses.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleDescriptors",
                columns: table => new
                {
                    RoleDescriptorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDescriptors", x => x.RoleDescriptorId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypeDescriptors",
                columns: table => new
                {
                    TransactionTypeDescriptorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypeDescriptors", x => x.TransactionTypeDescriptorId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleDescriptorId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_RoleDescriptors_RoleDescriptorId",
                        column: x => x.RoleDescriptorId,
                        principalTable: "RoleDescriptors",
                        principalColumn: "RoleDescriptorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionCategoryDescriptors",
                columns: table => new
                {
                    TransactionCategoryDescriptorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeDescriptorId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategoryDescriptors", x => x.TransactionCategoryDescriptorId);
                    table.ForeignKey(
                        name: "FK_TransactionCategoryDescriptors_TransactionTypeDescriptors_TransactionTypeDescriptorId",
                        column: x => x.TransactionTypeDescriptorId,
                        principalTable: "TransactionTypeDescriptors",
                        principalColumn: "TransactionTypeDescriptorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionSubCategoryDescriptors",
                columns: table => new
                {
                    TransactionSubCategoryDescriptorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionCategoryDescriptorId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionSubCategoryDescriptors", x => x.TransactionSubCategoryDescriptorId);
                    table.ForeignKey(
                        name: "FK_TransactionSubCategoryDescriptors_TransactionCategoryDescriptors_TransactionCategoryDescriptorId",
                        column: x => x.TransactionCategoryDescriptorId,
                        principalTable: "TransactionCategoryDescriptors",
                        principalColumn: "TransactionCategoryDescriptorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionEntries",
                columns: table => new
                {
                    TransactionEntryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionCategoryDescriptorOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionSubCategoryDescriptorOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionCategoryDescriptorId = table.Column<int>(type: "int", nullable: false),
                    TransactionSubCategoryDescriptorId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionEntries", x => x.TransactionEntryId);
                    table.ForeignKey(
                        name: "FK_TransactionEntries_TransactionCategoryDescriptors_TransactionCategoryDescriptorId",
                        column: x => x.TransactionCategoryDescriptorId,
                        principalTable: "TransactionCategoryDescriptors",
                        principalColumn: "TransactionCategoryDescriptorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionEntries_TransactionSubCategoryDescriptors_TransactionSubCategoryDescriptorId",
                        column: x => x.TransactionSubCategoryDescriptorId,
                        principalTable: "TransactionSubCategoryDescriptors",
                        principalColumn: "TransactionSubCategoryDescriptorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategoryDescriptors_TransactionTypeDescriptorId",
                table: "TransactionCategoryDescriptors",
                column: "TransactionTypeDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_TransactionCategoryDescriptorId",
                table: "TransactionEntries",
                column: "TransactionCategoryDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_TransactionSubCategoryDescriptorId",
                table: "TransactionEntries",
                column: "TransactionSubCategoryDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_UserId",
                table: "TransactionEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSubCategoryDescriptors_TransactionCategoryDescriptorId",
                table: "TransactionSubCategoryDescriptors",
                column: "TransactionCategoryDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleDescriptorId",
                table: "Users",
                column: "RoleDescriptorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionEntries");

            migrationBuilder.DropTable(
                name: "TransactionSubCategoryDescriptors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TransactionCategoryDescriptors");

            migrationBuilder.DropTable(
                name: "RoleDescriptors");

            migrationBuilder.DropTable(
                name: "TransactionTypeDescriptors");
        }
    }
}
