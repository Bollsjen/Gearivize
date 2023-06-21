using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerivize.Migrations
{
    /// <inheritdoc />
    public partial class templateConnector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
