using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MODIFIED_TABLE_COLUMN_NAMES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Customers_customer_id",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_customer_id",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "tradesperson_id",
                table: "Tradespersons",
                newName: "TradespersonId");

            migrationBuilder.RenameColumn(
                name: "job_id",
                table: "Jobs",
                newName: "JobId");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "clientType_id",
                table: "ClientTypes",
                newName: "ClientTypeId");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "AspNetUsers",
                newName: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CustomerId",
                table: "AspNetUsers",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Customers_CustomerId",
                table: "AspNetUsers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Customers_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TradespersonId",
                table: "Tradespersons",
                newName: "tradesperson_id");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "Jobs",
                newName: "job_id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customers",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "ClientTypeId",
                table: "ClientTypes",
                newName: "clientType_id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "AspNetUsers",
                newName: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_customer_id",
                table: "AspNetUsers",
                column: "customer_id",
                unique: true,
                filter: "[customer_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Customers_customer_id",
                table: "AspNetUsers",
                column: "customer_id",
                principalTable: "Customers",
                principalColumn: "customer_id");
        }
    }
}
