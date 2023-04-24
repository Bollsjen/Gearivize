using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerivize.Migrations
{
    /// <inheritdoc />
    public partial class instrument_image_source : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_source",
                table: "instrument",
                type: "longtext",
                nullable: true,
                collation: "utf8mb3_general_ci")
                .Annotation("MySql:CharSet", "utf8mb3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_source",
                table: "instrument");
        }
    }
}
