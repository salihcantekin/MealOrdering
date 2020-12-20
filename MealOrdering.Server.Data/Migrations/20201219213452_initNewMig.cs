using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MealOrdering.Server.Data.Migrations
{
    public partial class initNewMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "suppliers",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    name = table.Column<string>(type: "character varying", maxLength: 100, nullable: true),
                    web_url = table.Column<string>(type: "character varying", maxLength: 500, nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "UUID_GENERATE_V4()"),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    first_name = table.Column<string>(type: "character varying", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "character varying", maxLength: 100, nullable: true),
                    email_address = table.Column<string>(type: "character varying", maxLength: 100, nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    created_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying", maxLength: 1000, nullable: true),
                    expire_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_order_id",
                        column: x => x.created_user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_supplier_order_id",
                        column: x => x.supplier_id,
                        principalSchema: "public",
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    created_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orderItem_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_orderitems_user_id",
                        column: x => x.created_user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orderitems_order_id",
                        column: x => x.order_id,
                        principalSchema: "public",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_items_created_user_id",
                schema: "public",
                table: "order_items",
                column: "created_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                schema: "public",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_created_user_id",
                schema: "public",
                table: "orders",
                column: "created_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_supplier_id",
                schema: "public",
                table: "orders",
                column: "supplier_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items",
                schema: "public");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "suppliers",
                schema: "public");
        }
    }
}
