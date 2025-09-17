using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketNet.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdGenerationAddAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_attributes",
                columns: table => new
                {
                    product_attribute_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_attribute_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    product_attribute_value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    product_attribute_product_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_attributes", x => x.product_attribute_id);
                    table.ForeignKey(
                        name: "FK_product_attributes_product_product_attribute_product_id",
                        column: x => x.product_attribute_product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_attributes_product_attribute_product_id_product_att~",
                table: "product_attributes",
                columns: new[] { "product_attribute_product_id", "product_attribute_name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_attributes");
        }
    }
}
