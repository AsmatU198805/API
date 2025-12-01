using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddFKtoPurchaseRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseRequestmodels",
                columns: table => new
                {
                    PRID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PRQuantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MUnitId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequestmodels", x => x.PRID);
                    table.ForeignKey(
                        name: "FK_PurchaseRequestmodels_Munit_MUnitId",
                        column: x => x.MUnitId,
                        principalTable: "Munit",
                        principalColumn: "MUnitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseRequestmodels_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestmodels_Id",
                table: "PurchaseRequestmodels",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestmodels_MUnitId",
                table: "PurchaseRequestmodels",
                column: "MUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseRequestmodels");
        }
    }
}
