using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CategorySeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId", "ProductsIds" },
                values: new object[,]
                {
                    { new Guid("3a58148c-a581-4827-a25f-5d6805327cd7"), "Различные комплектующие для сборки персонального компьютера", "Комплектующие пк", null, null },
                    { new Guid("f68e9e8f-b487-48a8-be07-1ed67e67ecc1"), "Компьютерная переферия для работы и игр", "Компьютерная переферия", null, null },
                    { new Guid("63962daf-d3c4-4fba-9d14-9579f24eca0f"), "Беспроводные, игровые, офисные наушники", "Наушники", new Guid("f68e9e8f-b487-48a8-be07-1ed67e67ecc1"), null },
                    { new Guid("d0cd08bd-ede4-49de-b862-1163817341da"), "Видеопроцессоры для компьютера", "Видеокарты", new Guid("3a58148c-a581-4827-a25f-5d6805327cd7"), null },
                    { new Guid("d594553a-b10f-47f8-8441-3a51dd1904a7"), "Компьютерные клавиатуры, игровые клавиатуры", "Клавиатуры", new Guid("f68e9e8f-b487-48a8-be07-1ed67e67ecc1"), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("63962daf-d3c4-4fba-9d14-9579f24eca0f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d0cd08bd-ede4-49de-b862-1163817341da"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d594553a-b10f-47f8-8441-3a51dd1904a7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3a58148c-a581-4827-a25f-5d6805327cd7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f68e9e8f-b487-48a8-be07-1ed67e67ecc1"));
        }
    }
}
