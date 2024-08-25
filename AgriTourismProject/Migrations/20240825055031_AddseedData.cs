using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriTourismProject.Migrations
{
    /// <inheritdoc />
    public partial class AddseedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Capacity", "Cost", "Date", "Location", "Name" },
                values: new object[,]
                {
                    { 1, 10, 100.0, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Countryside", "Farm Stay" },
                    { 2, 20, 150.0, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wine Country", "Vineyard Tour" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
