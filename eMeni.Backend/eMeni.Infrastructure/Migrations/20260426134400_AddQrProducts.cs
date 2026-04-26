using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMeni.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddQrProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop FK that references the old table name before renaming
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_QRCodeProducts",
                table: "OrderItemEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QRCodeProductEntity",
                table: "QRCodeProductEntity");

            migrationBuilder.RenameTable(
                name: "QRCodeProductEntity",
                newName: "QrProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QrProducts",
                table: "QrProducts",
                column: "Id");

            // Re-add FK pointing to the renamed table
            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_QRCodeProducts",
                table: "OrderItemEntity",
                column: "QRCodeProductId",
                principalTable: "QrProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_QRCodeProducts",
                table: "OrderItemEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QrProducts",
                table: "QrProducts");

            migrationBuilder.RenameTable(
                name: "QrProducts",
                newName: "QRCodeProductEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QRCodeProductEntity",
                table: "QRCodeProductEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_QRCodeProducts",
                table: "OrderItemEntity",
                column: "QRCodeProductId",
                principalTable: "QRCodeProductEntity",
                principalColumn: "Id");
        }
    }
}
