using Microsoft.EntityFrameworkCore.Migrations;

namespace EWalletV2.DataAccess.Migrations
{
    public partial class fk_other_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transactions_other_id",
                table: "Transactions",
                column: "other_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_other_id",
                table: "Transactions",
                column: "other_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_other_id",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_other_id",
                table: "Transactions");
        }
    }
}
