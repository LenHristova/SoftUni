using Microsoft.EntityFrameworkCore.Migrations;

namespace P03_SalesDatabase.Data.Migrations
{
    public partial class RenameCustomersColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreaditCardNumber",
                table: "Customers",
                newName: "CreditCardNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
