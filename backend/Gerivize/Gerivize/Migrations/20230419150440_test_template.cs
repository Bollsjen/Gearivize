using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerivize.Migrations
{
    /// <inheritdoc />
    public partial class test_template : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "test_template_id",
                table: "instrument",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "test_template",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_template", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_instrument_test_template_id",
                table: "instrument",
                column: "test_template_id");

            migrationBuilder.AddForeignKey(
                name: "FK_instrument_test_template_test_template_id",
                table: "instrument",
                column: "test_template_id",
                principalTable: "test_template",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instrument_test_template_test_template_id",
                table: "instrument");

            migrationBuilder.DropTable(
                name: "test_template");

            migrationBuilder.DropIndex(
                name: "IX_instrument_test_template_id",
                table: "instrument");

            migrationBuilder.DropColumn(
                name: "test_template_id",
                table: "instrument");
        }
    }
}
