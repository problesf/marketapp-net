using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketNet.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    category_slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    category_description = table.Column<string>(type: "text", nullable: false),
                    category_parent_category_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.category_id);
                    table.ForeignKey(
                        name: "FK_categories_categories_category_parent_category_id",
                        column: x => x.category_parent_category_id,
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    user_password_hash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "seller_profiles",
                columns: table => new
                {
                    seller_profile_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    seller_profile_user_id = table.Column<long>(type: "bigint", nullable: false),
                    seller_profile_store_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    seller_profile_tax_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    seller_profile_payout_account = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seller_profiles", x => x.seller_profile_id);
                    table.ForeignKey(
                        name: "FK_seller_profiles_users_seller_profile_user_id",
                        column: x => x.seller_profile_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    product_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    product_name = table.Column<string>(type: "text", nullable: false),
                    product_description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    product_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    product_stock = table.Column<int>(type: "integer", nullable: false),
                    product_tax_rate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    product_currency = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    product_is_active = table.Column<bool>(type: "boolean", nullable: false),
                    product_seller_profile_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_product_seller_profiles_product_seller_profile_id",
                        column: x => x.product_seller_profile_id,
                        principalTable: "seller_profiles",
                        principalColumn: "seller_profile_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoryProduct_categories_id = table.Column<long>(type: "bigint", nullable: false),
                    CategoryProduct_products_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoryProduct_categories_id, x.CategoryProduct_products_id });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_categories_CategoryProduct_categories_id",
                        column: x => x.CategoryProduct_categories_id,
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_product_CategoryProduct_products_id",
                        column: x => x.CategoryProduct_products_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventory_movements",
                columns: table => new
                {
                    inventory_movement_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inventory_movement_product_id = table.Column<long>(type: "bigint", nullable: false),
                    inventory_movement_quantity = table.Column<int>(type: "integer", nullable: false),
                    inventory_movement_reason = table.Column<int>(type: "integer", nullable: false),
                    inventory_movement_reference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_movements", x => x.inventory_movement_id);
                    table.ForeignKey(
                        name: "FK_inventory_movements_product_inventory_movement_product_id",
                        column: x => x.inventory_movement_product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    address_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address_line1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    address_line2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    address_city = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    address_state = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    address_country = table.Column<string>(type: "character varying(2)", unicode: false, maxLength: 2, nullable: false),
                    address_postal_code = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    address_is_default_billing = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    address_is_default_shipping = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    address_customer_profile_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "customer_profiles",
                columns: table => new
                {
                    customer_profile_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_profile_user_id = table.Column<long>(type: "bigint", nullable: false),
                    customer_profile_default_billing_address_id = table.Column<long>(type: "bigint", nullable: true),
                    customer_profile_default_shipping_address_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_profiles", x => x.customer_profile_id);
                    table.ForeignKey(
                        name: "FK_customer_profiles_addresses_customer_profile_default_billin~",
                        column: x => x.customer_profile_default_billing_address_id,
                        principalTable: "addresses",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customer_profiles_addresses_customer_profile_default_shippi~",
                        column: x => x.customer_profile_default_shipping_address_id,
                        principalTable: "addresses",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customer_profiles_users_customer_profile_user_id",
                        column: x => x.customer_profile_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(type: "integer", nullable: false),
                    order_status = table.Column<string>(type: "character varying(32)", unicode: false, maxLength: 32, nullable: false),
                    order_sub_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    order_tax_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    order_shipping_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    order_discount_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    order_grand_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    order_currency = table.Column<string>(type: "character varying(3)", unicode: false, maxLength: 3, nullable: false),
                    order_place_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    order_paid_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    order_cancelled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    order_delivered_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    order_line1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    order_line2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    order_city = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    order_state = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    order_postal_code = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    order_country = table.Column<string>(type: "character varying(2)", unicode: false, maxLength: 2, nullable: false),
                    order_customer_profile_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_orders_customer_profiles_order_customer_profile_id",
                        column: x => x.order_customer_profile_id,
                        principalTable: "customer_profiles",
                        principalColumn: "customer_profile_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    review_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    review_product_id = table.Column<long>(type: "bigint", nullable: false),
                    review_rating = table.Column<int>(type: "integer", nullable: false),
                    review_comment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    review_is_approved = table.Column<bool>(type: "boolean", nullable: false),
                    review_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    review_customer_profile_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_reviews_customer_profiles_review_customer_profile_id",
                        column: x => x.review_customer_profile_id,
                        principalTable: "customer_profiles",
                        principalColumn: "customer_profile_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reviews_product_review_product_id",
                        column: x => x.review_product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    order_item_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_item_order_id = table.Column<long>(type: "bigint", nullable: false),
                    order_item_product_id = table.Column<long>(type: "bigint", nullable: false),
                    order_item_code_snapshot = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    order_item_product_name_snapshot = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    order_item_unit_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    order_item_quantity = table.Column<int>(type: "integer", nullable: false),
                    order_item_tax_rate = table.Column<decimal>(type: "numeric(5,4)", nullable: false),
                    order_item_line_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.order_item_id);
                    table.ForeignKey(
                        name: "FK_order_items_orders_order_item_order_id",
                        column: x => x.order_item_order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_items_product_order_item_product_id",
                        column: x => x.order_item_product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    payment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payment_order_id = table.Column<long>(type: "bigint", nullable: false),
                    payment_provider = table.Column<string>(type: "text", nullable: false),
                    payment_status = table.Column<string>(type: "text", nullable: false),
                    payment_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    payment_currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    payment_external_reference = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    payment_ocurred_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK_payments_orders_payment_order_id",
                        column: x => x.payment_order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shipments",
                columns: table => new
                {
                    shipment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    shipment_order_id = table.Column<long>(type: "bigint", nullable: false),
                    shipment_carrier = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    shipment_tracking_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    shipment_status = table.Column<string>(type: "text", nullable: false),
                    shipment_shipped_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    shipment_delivered_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    shipment_cost = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipments", x => x.shipment_id);
                    table.ForeignKey(
                        name: "FK_shipments_orders_shipment_order_id",
                        column: x => x.shipment_order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_customerprofileid",
                table: "addresses",
                column: "address_customer_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_default_billing",
                table: "addresses",
                columns: new[] { "address_customer_profile_id", "address_is_default_billing" });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_default_shipping",
                table: "addresses",
                columns: new[] { "address_customer_profile_id", "address_is_default_shipping" });

            migrationBuilder.CreateIndex(
                name: "IX_categories_category_parent_category_id",
                table: "categories",
                column: "category_parent_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_category_slug",
                table: "categories",
                column: "category_slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_CategoryProduct_products_id",
                table: "CategoryProduct",
                column: "CategoryProduct_products_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_profiles_customer_profile_default_billing_address_~",
                table: "customer_profiles",
                column: "customer_profile_default_billing_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_profiles_customer_profile_default_shipping_address~",
                table: "customer_profiles",
                column: "customer_profile_default_shipping_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_profiles_customer_profile_user_id",
                table: "customer_profiles",
                column: "customer_profile_user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_inventory_movement_product_id",
                table: "inventory_movements",
                column: "inventory_movement_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_item_order_id",
                table: "order_items",
                column: "order_item_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_item_product_id",
                table: "order_items",
                column: "order_item_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_order_customer_profile_id",
                table: "orders",
                column: "order_customer_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_ordernumber",
                table: "orders",
                column: "order_number");

            migrationBuilder.CreateIndex(
                name: "ix_orders_placeat_status",
                table: "orders",
                columns: new[] { "order_place_at", "order_status" });

            migrationBuilder.CreateIndex(
                name: "IX_payments_payment_order_id",
                table: "payments",
                column: "payment_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_product_code",
                table: "product",
                column: "product_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_product_seller_profile_id",
                table: "product",
                column: "product_seller_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_review_customer_profile_id",
                table: "reviews",
                column: "review_customer_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_review_product_id",
                table: "reviews",
                column: "review_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_seller_profiles_seller_profile_user_id",
                table: "seller_profiles",
                column: "seller_profile_user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shipments_shipment_order_id",
                table: "shipments",
                column: "shipment_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_user_email",
                table: "users",
                column: "user_email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_customer_profiles_address_customer_profile_id",
                table: "addresses",
                column: "address_customer_profile_id",
                principalTable: "customer_profiles",
                principalColumn: "customer_profile_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_customer_profiles_address_customer_profile_id",
                table: "addresses");

            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "inventory_movements");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "shipments");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "seller_profiles");

            migrationBuilder.DropTable(
                name: "customer_profiles");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
