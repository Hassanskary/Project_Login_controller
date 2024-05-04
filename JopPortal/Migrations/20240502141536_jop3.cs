using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JopPortal.Migrations
{
    public partial class jop3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyPhoto",
                table: "company");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "company",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "company");

            migrationBuilder.AddColumn<byte[]>(
                name: "CompanyPhoto",
                table: "company",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
