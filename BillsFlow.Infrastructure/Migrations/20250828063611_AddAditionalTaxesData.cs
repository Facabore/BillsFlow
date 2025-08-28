using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillsFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAditionalTaxesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "tax",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tax",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "tax",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeletedOnUtc", "IsDeleted" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "tax",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeletedOnUtc", "IsDeleted" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "tax",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DeletedOnUtc", "IsDeleted" },
                values: new object[] { null, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "tax");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tax");
        }
    }
}
