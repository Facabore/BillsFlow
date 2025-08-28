using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillsFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonalInfo_FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PersonalInfo_LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PersonalInfo_Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    PersonalInfo_DocumentType = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    PersonalInfo_DocumentNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ContactInfo_PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ContactInfo_Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
