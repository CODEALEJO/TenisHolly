using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TenisHolly.Migrations
{
    /// <inheritdoc />
    public partial class FixedLoanRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    location = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stores", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    reference = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    size = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_shoes_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventoryes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false),
                    shoe_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventoryes", x => x.id);
                    table.ForeignKey(
                        name: "FK_inventoryes_shoes_shoe_id",
                        column: x => x.shoe_id,
                        principalTable: "shoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventoryes_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "loans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    loan_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    return_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    from_store_id = table.Column<int>(type: "int", nullable: false),
                    to_store_id = table.Column<int>(type: "int", nullable: false),
                    shoe_id = table.Column<int>(type: "int", nullable: false),
                    sizes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loans", x => x.id);
                    table.ForeignKey(
                        name: "FK_loans_shoes_shoe_id",
                        column: x => x.shoe_id,
                        principalTable: "shoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_loans_stores_from_store_id",
                        column: x => x.from_store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_loans_stores_to_store_id",
                        column: x => x.to_store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    payment_method = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    seller = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    shoe_id = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.id);
                    table.ForeignKey(
                        name: "FK_sales_shoes_shoe_id",
                        column: x => x.shoe_id,
                        principalTable: "shoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sales_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "stores",
                columns: new[] { "id", "location", "name" },
                values: new object[,]
                {
                    { 1, "Centro Comercial Aventura", "Tienda Central" },
                    { 2, "Mall Norte Plaza", "Tienda Norte" },
                    { 3, "Centro Comercial del Sur", "Tienda Sur" },
                    { 4, "Plaza Este", "Tienda Este" },
                    { 5, "Centro Comercial Oeste", "Tienda Oeste" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "name", "password_hash", "role" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "Admin User", "hashedpassword1", "Admin" },
                    { 2, "juan.perez@example.com", "Juan Pérez", "hashedpassword2", "User" },
                    { 3, "maria.gomez@example.com", "María Gómez", "hashedpassword3", "User" },
                    { 4, "carlos.lopez@example.com", "Carlos López", "hashedpassword4", "Manager" },
                    { 5, "ana.martinez@example.com", "Ana Martínez", "hashedpassword5", "User" }
                });

            migrationBuilder.InsertData(
                table: "shoes",
                columns: new[] { "id", "gender", "price", "reference", "size", "stock", "store_id" },
                values: new object[,]
                {
                    { 1, "Caballero", 120.50m, "Nike AirMax", 42, 10, 1 },
                    { 2, "Dama", 95.99m, "Adidas Superstar", 38, 15, 2 },
                    { 3, "Caballero", 80.00m, "Puma Classic", 40, 20, 3 },
                    { 4, "Dama", 75.99m, "Reebok Runner", 39, 12, 4 },
                    { 5, "Caballero", 110.00m, "New Balance 574", 41, 8, 5 }
                });

            migrationBuilder.InsertData(
                table: "inventoryes",
                columns: new[] { "id", "quantity", "shoe_id", "store_id" },
                values: new object[,]
                {
                    { 1, 50, 1, 1 },
                    { 2, 40, 2, 2 },
                    { 3, 30, 3, 3 },
                    { 4, 20, 4, 4 },
                    { 5, 25, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "loans",
                columns: new[] { "id", "from_store_id", "loan_date", "quantity", "return_date", "shoe_id", "sizes", "status", "to_store_id" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 31, 14, 20, 59, 93, DateTimeKind.Local).AddTicks(5789), 5, null, 1, "42", 0, 2 },
                    { 2, 2, new DateTime(2025, 1, 31, 14, 20, 59, 93, DateTimeKind.Local).AddTicks(5793), 3, null, 2, "38", 0, 3 },
                    { 3, 3, new DateTime(2025, 1, 31, 14, 20, 59, 93, DateTimeKind.Local).AddTicks(5798), 2, null, 3, "40", 1, 4 },
                    { 4, 4, new DateTime(2025, 1, 31, 14, 20, 59, 93, DateTimeKind.Local).AddTicks(5800), 4, null, 4, "39", 2, 5 },
                    { 5, 5, new DateTime(2025, 1, 31, 14, 20, 59, 93, DateTimeKind.Local).AddTicks(5802), 1, null, 5, "41", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "sales",
                columns: new[] { "id", "date", "payment_method", "quantity", "seller", "shoe_id", "store_id", "total" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 31, 14, 20, 59, 93, DateTimeKind.Local).AddTicks(5747), "Tarjeta", 2, "Carlos", 1, 1, 241.00m },
                    { 2, new DateTime(2025, 1, 31, 14, 20, 59, 93, DateTimeKind.Local).AddTicks(5762), "Efectivo", 1, "Andrea", 2, 2, 95.99m },
                    { 3, new DateTime(2025, 1, 31, 14, 20, 59, 93, DateTimeKind.Local).AddTicks(5764), "Transferencia", 3, "Pedro", 3, 3, 240.00m },
                    { 4, new DateTime(2025, 1, 31, 14, 20, 59, 93, DateTimeKind.Local).AddTicks(5766), "Tarjeta", 1, "Lucía", 4, 4, 75.99m },
                    { 5, new DateTime(2025, 1, 31, 14, 20, 59, 93, DateTimeKind.Local).AddTicks(5767), "Efectivo", 2, "Miguel", 5, 5, 220.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_inventoryes_shoe_id",
                table: "inventoryes",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventoryes_store_id",
                table: "inventoryes",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_loans_from_store_id",
                table: "loans",
                column: "from_store_id");

            migrationBuilder.CreateIndex(
                name: "IX_loans_shoe_id",
                table: "loans",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "IX_loans_to_store_id",
                table: "loans",
                column: "to_store_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_shoe_id",
                table: "sales",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_store_id",
                table: "sales",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoes_store_id",
                table: "shoes",
                column: "store_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventoryes");

            migrationBuilder.DropTable(
                name: "loans");

            migrationBuilder.DropTable(
                name: "sales");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "shoes");

            migrationBuilder.DropTable(
                name: "stores");
        }
    }
}
