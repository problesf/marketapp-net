using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketNet.Migrations
{
    /// <inheritdoc />
    public partial class AddActivateAttributeToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "category_is_active",
                table: "categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category_is_active",
                table: "categories");
        }
    }
}
