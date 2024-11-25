using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesLab1.Migrations
{
    /// <inheritdoc />
    public partial class AddTranscation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionTId",
                table: "BankAccounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sourceAccNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TId);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_AId",
                        column: x => x.AId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_TransactionTId",
                table: "BankAccounts",
                column: "TransactionTId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AId",
                table: "Transactions",
                column: "AId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Transactions_TransactionTId",
                table: "BankAccounts",
                column: "TransactionTId",
                principalTable: "Transactions",
                principalColumn: "TId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Transactions_TransactionTId",
                table: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_TransactionTId",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "TransactionTId",
                table: "BankAccounts");
        }
    }
}
