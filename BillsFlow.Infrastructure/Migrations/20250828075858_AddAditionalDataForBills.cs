using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillsFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAditionalDataForBills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaxId",
                table: "bill_detail",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "bill_detail");
        }
    }
}
