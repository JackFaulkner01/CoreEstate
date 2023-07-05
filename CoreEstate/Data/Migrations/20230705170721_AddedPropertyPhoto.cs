using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreEstate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertyPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filename = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ForSalePropertyId = table.Column<int>(type: "int", nullable: true),
                    ToRentPropertyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyPhotos_ForSaleProperties_ForSalePropertyId",
                        column: x => x.ForSalePropertyId,
                        principalTable: "ForSaleProperties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyPhotos_ToRentProperties_ToRentPropertyId",
                        column: x => x.ToRentPropertyId,
                        principalTable: "ToRentProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyPhotos_ForSalePropertyId",
                table: "PropertyPhotos",
                column: "ForSalePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyPhotos_ToRentPropertyId",
                table: "PropertyPhotos",
                column: "ToRentPropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyPhotos");
        }
    }
}
