using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class ManyToManyRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryProduct",
                table: "CategoryProduct");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("62f28ca9-a2e8-49d6-b215-11000ae6c713"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8091fd41-67fb-47e7-b8fe-007156101f64"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9d092ec8-97b2-4961-9d19-28d7149bbece"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e14d324f-e80a-4ad9-b337-cf05621c0ba0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f7325b0a-23ac-4d60-b624-f6004a58ae0e"));

            migrationBuilder.RenameTable(
                name: "CategoryProduct",
                newName: "ProductCategories");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "ProductCategories",
                newName: "IX_ProductCategories_ProductsId");

            migrationBuilder.AddColumn<string>(
                name: "ChildrenCategoriesIds",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                columns: new[] { "CategoriesId", "ProductsId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoriesId",
                table: "ProductCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_ProductsId",
                table: "ProductCategories",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoriesId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_ProductsId",
                table: "ProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

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

            migrationBuilder.DropColumn(
                name: "ChildrenCategoriesIds",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "CategoryProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_ProductsId",
                table: "CategoryProduct",
                newName: "IX_CategoryProduct_ProductsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryProduct",
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "ProductsIds" },
                values: new object[,]
                {
                    { new Guid("e14d324f-e80a-4ad9-b337-cf05621c0ba0"), "Компьютерная переферия", null, null },
                    { new Guid("f7325b0a-23ac-4d60-b624-f6004a58ae0e"), "Комплектующие пк", null, null },
                    { new Guid("62f28ca9-a2e8-49d6-b215-11000ae6c713"), "Видеокарты", new Guid("f7325b0a-23ac-4d60-b624-f6004a58ae0e"), null },
                    { new Guid("8091fd41-67fb-47e7-b8fe-007156101f64"), "Клавиатуры", new Guid("e14d324f-e80a-4ad9-b337-cf05621c0ba0"), null },
                    { new Guid("9d092ec8-97b2-4961-9d19-28d7149bbece"), "Наушники", new Guid("e14d324f-e80a-4ad9-b337-cf05621c0ba0"), null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
