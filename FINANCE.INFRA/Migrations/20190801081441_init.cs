using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FINANCE.INFRA.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Borrower",
                columns: table => new
                {
                    BorrowerID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    DoB = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: false),
                    IdCardNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 250, nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrower", x => x.BorrowerID);
                });

            migrationBuilder.CreateTable(
                name: "Contributor",
                columns: table => new
                {
                    ContributorID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    DoB = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    IdCardNumber = table.Column<int>(nullable: false),
                    BankNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributor", x => x.ContributorID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 16, nullable: false),
                    Avatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentLoanContract",
                columns: table => new
                {
                    ContractID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BorrowerID = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    InterestRate = table.Column<decimal>(nullable: false),
                    ContractSignDate = table.Column<DateTime>(nullable: false),
                    Term = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 8, 1, 15, 14, 41, 631, DateTimeKind.Local).AddTicks(7937)),
                    DailyInterest = table.Column<decimal>(nullable: false),
                    InterestCycle = table.Column<int>(nullable: false),
                    Paid = table.Column<decimal>(nullable: false),
                    Unpaid = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentLoanContract", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_InstallmentLoanContract_Borrower_BorrowerID",
                        column: x => x.BorrowerID,
                        principalTable: "Borrower",
                        principalColumn: "BorrowerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanContract",
                columns: table => new
                {
                    ContractID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BorrowerID = table.Column<int>(nullable: false),
                    LoanPackage = table.Column<int>(nullable: false, defaultValue: 0),
                    InterestRate = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ContractSignDate = table.Column<DateTime>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    InterestPayDate = table.Column<decimal>(nullable: false),
                    InterestInDebt = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    Note = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanContract", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_LoanContract_Borrower_BorrowerID",
                        column: x => x.BorrowerID,
                        principalTable: "Borrower",
                        principalColumn: "BorrowerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContributionContract",
                columns: table => new
                {
                    ContractID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContributorID = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    InterestRate = table.Column<decimal>(nullable: false),
                    ContractSignDate = table.Column<DateTime>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    InterestCycle = table.Column<int>(nullable: false),
                    ThisTermInterest = table.Column<decimal>(nullable: false),
                    NotReceivedInterest = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributionContract", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_ContributionContract_Contributor_ContributorID",
                        column: x => x.ContributorID,
                        principalTable: "Contributor",
                        principalColumn: "ContributorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentLoanTransactionHistory",
                columns: table => new
                {
                    ContractID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    InstallmentLoanContractID = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false, defaultValue: 0),
                    Amount = table.Column<decimal>(nullable: false),
                    ContractSignDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentLoanTransactionHistory", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_InstallmentLoanTransactionHistory_InstallmentLoanContract",
                        column: x => x.InstallmentLoanContractID,
                        principalTable: "InstallmentLoanContract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstallmentLoanTransactionHistory_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanTransactionHistory",
                columns: table => new
                {
                    ContractID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    LoanContractID = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false, defaultValue: 0),
                    Amount = table.Column<decimal>(nullable: false),
                    ContractSignDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTransactionHistory", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_LoanTransactionHistory_LoanContract",
                        column: x => x.LoanContractID,
                        principalTable: "LoanContract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanTransactionHistory_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContributionTransactionHistory",
                columns: table => new
                {
                    ContractID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    ContributionContractID = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false, defaultValue: 0),
                    Amount = table.Column<decimal>(nullable: false),
                    ContractSignDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributionTransactionHistory", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_ContributionTransactionHistory_ContributionContract",
                        column: x => x.ContributionContractID,
                        principalTable: "ContributionContract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContributionTransactionHistory_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContributionContract_ContributorID",
                table: "ContributionContract",
                column: "ContributorID");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionTransactionHistory_ContributionContractID",
                table: "ContributionTransactionHistory",
                column: "ContributionContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionTransactionHistory_UserID",
                table: "ContributionTransactionHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentLoanContract_BorrowerID",
                table: "InstallmentLoanContract",
                column: "BorrowerID");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentLoanTransactionHistory_InstallmentLoanContractID",
                table: "InstallmentLoanTransactionHistory",
                column: "InstallmentLoanContractID");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentLoanTransactionHistory_UserID",
                table: "InstallmentLoanTransactionHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanContract_BorrowerID",
                table: "LoanContract",
                column: "BorrowerID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTransactionHistory_LoanContractID",
                table: "LoanTransactionHistory",
                column: "LoanContractID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTransactionHistory_UserID",
                table: "LoanTransactionHistory",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContributionTransactionHistory");

            migrationBuilder.DropTable(
                name: "InstallmentLoanTransactionHistory");

            migrationBuilder.DropTable(
                name: "LoanTransactionHistory");

            migrationBuilder.DropTable(
                name: "ContributionContract");

            migrationBuilder.DropTable(
                name: "InstallmentLoanContract");

            migrationBuilder.DropTable(
                name: "LoanContract");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Contributor");

            migrationBuilder.DropTable(
                name: "Borrower");
        }
    }
}
