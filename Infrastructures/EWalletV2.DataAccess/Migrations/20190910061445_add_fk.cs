using Microsoft.EntityFrameworkCore.Migrations;

namespace EWalletV2.DataAccess.Migrations
{
    public partial class add_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_other_id",
                table: "Transactions");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_customer_id",
                table: "Transactions",
                column: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCustomerEntity",
                table: "Transactions",
                column: "customer_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOtherEntity",
                table: "Transactions",
                column: "other_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCustomerEntity",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOtherEntity",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_customer_id",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_other_id",
                table: "Transactions",
                column: "other_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
