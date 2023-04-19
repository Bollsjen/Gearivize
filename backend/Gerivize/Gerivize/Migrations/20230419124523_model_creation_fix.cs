using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerivize.Migrations
{
    /// <inheritdoc />
    public partial class model_creation_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_user_user_id",
                table: "Instruments");

            migrationBuilder.DropForeignKey(
                name: "FK_notification_Instruments_a_number",
                table: "notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruments",
                table: "Instruments");

            migrationBuilder.RenameTable(
                name: "Instruments",
                newName: "instrument");

            migrationBuilder.RenameIndex(
                name: "IX_Instruments_user_id",
                table: "instrument",
                newName: "IX_instrument_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_instrument",
                table: "instrument",
                column: "a_number");

            migrationBuilder.AddForeignKey(
                name: "FK_instrument_user_user_id",
                table: "instrument",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notification_instrument_a_number",
                table: "notification",
                column: "a_number",
                principalTable: "instrument",
                principalColumn: "a_number",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instrument_user_user_id",
                table: "instrument");

            migrationBuilder.DropForeignKey(
                name: "FK_notification_instrument_a_number",
                table: "notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_instrument",
                table: "instrument");

            migrationBuilder.RenameTable(
                name: "instrument",
                newName: "Instruments");

            migrationBuilder.RenameIndex(
                name: "IX_instrument_user_id",
                table: "Instruments",
                newName: "IX_Instruments_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruments",
                table: "Instruments",
                column: "a_number");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruments_user_user_id",
                table: "Instruments",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notification_Instruments_a_number",
                table: "notification",
                column: "a_number",
                principalTable: "Instruments",
                principalColumn: "a_number",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
