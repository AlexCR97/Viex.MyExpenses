using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Viex.MyExpenses.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryDescriptors",
                columns: table => new
                {
                    CategoryDescriptorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDescriptors", x => x.CategoryDescriptorId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypeDescriptors",
                columns: table => new
                {
                    TransactionTypeDescriptorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypeDescriptors", x => x.TransactionTypeDescriptorId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionEntries",
                columns: table => new
                {
                    TransactionEntryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionEntries", x => x.TransactionEntryId);
                    table.ForeignKey(
                        name: "FK_TransactionEntries_CategoryDescriptors_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryDescriptors",
                        principalColumn: "CategoryDescriptorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionEntries_TransactionTypeDescriptors_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TransactionTypeDescriptors",
                        principalColumn: "TransactionTypeDescriptorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_CategoryId",
                table: "TransactionEntries",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_TypeId",
                table: "TransactionEntries",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionEntries");

            migrationBuilder.DropTable(
                name: "CategoryDescriptors");

            migrationBuilder.DropTable(
                name: "TransactionTypeDescriptors");
        }
    }
}
