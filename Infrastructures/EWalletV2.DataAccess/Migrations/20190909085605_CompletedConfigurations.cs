using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EWalletV2.DataAccess.Migrations
{
    public partial class CompletedConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "Users",
                newName: "salt");

            migrationBuilder.RenameColumn(
                name: "Pin",
                table: "Users",
                newName: "pin");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Users",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Users",
                newName: "birthdate");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Users",
                newName: "balance");

            migrationBuilder.RenameColumn(
                name: "Account",
                table: "Users",
                newName: "account");

            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "Users",
                newName: "mobile_number");

            migrationBuilder.AlterColumn<string>(
                name: "salt",
                table: "Users",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pin",
                table: "Users",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "lastname",
                table: "Users",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "firstname",
                table: "Users",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Users",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "birthdate",
                table: "Users",
                maxLength: 100,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "account",
                table: "Users",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "mobile_number",
                table: "Users",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "payer_id",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "receiver_id",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Tokens",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "refresh_token",
                table: "Tokens",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Otps",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "otp",
                table: "Otps",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reference",
                table: "Otps",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "payer_id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "receiver_id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "refresh_token",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Otps");

            migrationBuilder.DropColumn(
                name: "otp",
                table: "Otps");

            migrationBuilder.DropColumn(
                name: "reference",
                table: "Otps");

            migrationBuilder.RenameColumn(
                name: "salt",
                table: "Users",
                newName: "Salt");

            migrationBuilder.RenameColumn(
                name: "pin",
                table: "Users",
                newName: "Pin");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Users",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "birthdate",
                table: "Users",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "balance",
                table: "Users",
                newName: "Balance");

            migrationBuilder.RenameColumn(
                name: "account",
                table: "Users",
                newName: "Account");

            migrationBuilder.RenameColumn(
                name: "mobile_number",
                table: "Users",
                newName: "MobileNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pin",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldMaxLength: 100,
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Account",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true);
        }
    }
}
