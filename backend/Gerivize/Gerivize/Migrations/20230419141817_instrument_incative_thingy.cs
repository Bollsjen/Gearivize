using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerivize.Migrations
{
    /// <inheritdoc />
    public partial class instrument_incative_thingy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "inactive",
                table: "instrument",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inactive",
                table: "instrument");
        }
    }
}
