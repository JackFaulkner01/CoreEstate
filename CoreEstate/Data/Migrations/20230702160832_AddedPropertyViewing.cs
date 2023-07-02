using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreEstate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertyViewing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyViewings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ForSalePropertyId = table.Column<int>(type: "int", nullable: true),
                    ToRentPropertyId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyViewings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyViewings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyViewings_ForSaleProperties_ForSalePropertyId",
                        column: x => x.ForSalePropertyId,
                        principalTable: "ForSaleProperties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyViewings_ToRentProperties_ToRentPropertyId",
                        column: x => x.ToRentPropertyId,
                        principalTable: "ToRentProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyViewings_ForSalePropertyId",
                table: "PropertyViewings",
                column: "ForSalePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyViewings_ToRentPropertyId",
                table: "PropertyViewings",
                column: "ToRentPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyViewings_UserId",
                table: "PropertyViewings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyViewings");
        }
    }
}
