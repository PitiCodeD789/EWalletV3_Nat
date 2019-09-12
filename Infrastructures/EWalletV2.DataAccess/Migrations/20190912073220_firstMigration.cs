using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EWalletV2.DataAccess.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Otps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    otp = table.Column<string>(maxLength: 100, nullable: true),
                    reference = table.Column<string>(maxLength: 100, nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    refresh_token = table.Column<string>(maxLength: 100, nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    firstname = table.Column<string>(maxLength: 100, nullable: true),
                    lastname = table.Column<string>(maxLength: 100, nullable: true),
                    birthdate = table.Column<DateTime>(maxLength: 100, nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    mobile_number = table.Column<string>(maxLength: 100, nullable: true),
                    gender = table.Column<int>(nullable: false),
                    email = table.Column<string>(maxLength: 100, nullable: false),
                    pin = table.Column<string>(maxLength: 100, nullable: true),
                    salt = table.Column<string>(maxLength: 100, nullable: true),
                    balance = table.Column<decimal>(nullable: false),
                    account = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_email", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    TransactionReference = table.Column<string>(nullable: true),
                    TransactionType = table.Column<int>(nullable: false),
                    customer_id = table.Column<int>(nullable: true),
                    other_id = table.Column<int>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCustomerEntity",
                        column: x => x.customer_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserOtherEntity",
                        column: x => x.other_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_customer_id",
                table: "Transactions",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_other_id",
                table: "Transactions",
                column: "other_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Otps");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
