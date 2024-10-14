using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class PricePrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("01f05d7e-170b-4768-8752-7776140820b6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bc4c1b47-9330-4aea-8ac0-b16d7395286d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c67a3731-62ce-430d-90ee-c0d67b166904"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c63a5741-b64d-4add-8135-e7f6e60d6a61"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d56ad7c9-ea52-4537-9e2d-454096e2f270"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ChildrenCategoriesIds", "Name", "ParentCategoryId", "ProductsIds" },
                values: new object[,]
                {
                    { new Guid("d02cb95e-3d18-471f-ad36-5397b7b1738e"), null, "Компьютерная переферия", null, null },
                    { new Guid("e7f27613-8e33-4b51-9ac1-48ca4751162b"), null, "Комплектующие пк", null, null },
                    { new Guid("72ca918f-7cc7-4f84-89e9-4a321b1479a9"), null, "Видеокарты", new Guid("e7f27613-8e33-4b51-9ac1-48ca4751162b"), null },
                    { new Guid("acbc9211-5aea-4840-8408-a190899cc122"), null, "Клавиатуры", new Guid("d02cb95e-3d18-471f-ad36-5397b7b1738e"), null },
                    { new Guid("d544378f-e8ad-4178-a846-316bc72a8bb5"), null, "Наушники", new Guid("d02cb95e-3d18-471f-ad36-5397b7b1738e"), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("72ca918f-7cc7-4f84-89e9-4a321b1479a9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("acbc9211-5aea-4840-8408-a190899cc122"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d544378f-e8ad-4178-a846-316bc72a8bb5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d02cb95e-3d18-471f-ad36-5397b7b1738e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e7f27613-8e33-4b51-9ac1-48ca4751162b"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldPrecision: 18,
                oldScale: 6);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ChildrenCategoriesIds", "Name", "ParentCategoryId", "ProductsIds" },
                values: new object[,]
                {
                    { new Guid("c63a5741-b64d-4add-8135-e7f6e60d6a61"), null, "Комплектующие пк", null, null },
                    { new Guid("d56ad7c9-ea52-4537-9e2d-454096e2f270"), null, "Компьютерная переферия", null, null },
                    { new Guid("01f05d7e-170b-4768-8752-7776140820b6"), null, "Видеокарты", new Guid("c63a5741-b64d-4add-8135-e7f6e60d6a61"), null },
                    { new Guid("bc4c1b47-9330-4aea-8ac0-b16d7395286d"), null, "Клавиатуры", new Guid("d56ad7c9-ea52-4537-9e2d-454096e2f270"), null },
                    { new Guid("c67a3731-62ce-430d-90ee-c0d67b166904"), null, "Наушники", new Guid("d56ad7c9-ea52-4537-9e2d-454096e2f270"), null }
                });
        }
    }
}
