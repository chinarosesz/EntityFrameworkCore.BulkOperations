﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCore.BulkOperations.Tests.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(nullable: false),
                    First = table.Column<string>(nullable: true),
                    Last = table.Column<string>(nullable: true),
                    Team = table.Column<string>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    HiredDate = table.Column<DateTime>(nullable: false),
                    UniqueName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeWithCompressedData",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(nullable: false),
                    First = table.Column<string>(nullable: true),
                    Last = table.Column<string>(nullable: true),
                    Team = table.Column<string>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    HiredDate = table.Column<DateTime>(nullable: false),
                    UniqueName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Data = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeWithCompressedData", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeWithData",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(nullable: false),
                    First = table.Column<string>(nullable: true),
                    Last = table.Column<string>(nullable: true),
                    Team = table.Column<string>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    HiredDate = table.Column<DateTime>(nullable: false),
                    UniqueName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeWithData", x => x.EmployeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "EmployeeWithCompressedData");

            migrationBuilder.DropTable(
                name: "EmployeeWithData");
        }
    }
}
