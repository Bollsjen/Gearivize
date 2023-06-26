using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerivize.Migrations
{
    /// <inheritdoc />
    public partial class instrument_clean_up_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "calibration_report_number",
                table: "instrument");

            migrationBuilder.DropColumn(
                name: "internal_calibration",
                table: "instrument");

            migrationBuilder.RenameColumn(
                name: "reference_calibration_instruction",
                table: "instrument",
                newName: "external_calibrator");

            migrationBuilder.CreateIndex(
                name: "IX_instrument_user_id2",
                table: "instrument",
                column: "user_id2");

            migrationBuilder.AddForeignKey(
                name: "FK_instrument_user_user_id2",
                table: "instrument",
                column: "user_id2",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instrument_user_user_id2",
                table: "instrument");

            migrationBuilder.DropIndex(
                name: "IX_instrument_user_id2",
                table: "instrument");

            migrationBuilder.RenameColumn(
                name: "external_calibrator",
                table: "instrument",
                newName: "reference_calibration_instruction");

            migrationBuilder.AddColumn<string>(
                name: "calibration_report_number",
                table: "instrument",
                type: "longtext",
                nullable: true,
                collation: "utf8mb3_general_ci")
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AddColumn<string>(
                name: "internal_calibration",
                table: "instrument",
                type: "longtext",
                nullable: true,
                collation: "utf8mb3_general_ci")
                .Annotation("MySql:CharSet", "utf8mb3");
        }
    }
}
