using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitakorProb.Migrations
{
    /// <inheritdoc />
    public partial class AddMandarinStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Mandarins",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Mandarins");
        }
    }
}
