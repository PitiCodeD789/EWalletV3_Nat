using Microsoft.EntityFrameworkCore.Migrations;

namespace EWalletV2.DataAccess.Migrations
{
    public partial class something : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "receiver_id",
                table: "Transactions",
                newName: "other_id");

            migrationBuilder.RenameColumn(
                name: "payer_id",
                table: "Transactions",
                newName: "customer_id");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionType",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Transactions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TransactionReference",
                table: "Transactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionReference",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "other_id",
                table: "Transactions",
                newName: "receiver_id");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Transactions",
                newName: "payer_id");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
