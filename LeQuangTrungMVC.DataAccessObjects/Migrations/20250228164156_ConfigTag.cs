using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeQuangTrungMVC.DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class ConfigTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CategoryName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CategoryDescription = table.Column<string>(type: "text", nullable: true),
                    ParentCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__19093A2B8AFE0FEB", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK_Category_ParentCategoryID",
                        column: x => x.ParentCategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "SystemAccount",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    AccountName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AccountEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AccountRole = table.Column<int>(type: "int", nullable: false),
                    AccountPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SystemAc__349DA586CF6622FA", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TagName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tag__657CFA4CFB003B55", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "NewsArticle",
                columns: table => new
                {
                    NewsArticleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    NewsTile = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Headline = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    NewsContent = table.Column<string>(type: "text", nullable: false),
                    NewsSource = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NewsArti__4CD0926C20CC2030", x => x.NewsArticleID);
                    table.ForeignKey(
                        name: "FK_NewsArticle_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK_NewsArticle_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "SystemAccount",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_NewsArticle_UpdatedByID",
                        column: x => x.UpdatedByID,
                        principalTable: "SystemAccount",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "NewsTag",
                columns: table => new
                {
                    NewsArticleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTag", x => new { x.NewsArticleID, x.TagID });
                    table.ForeignKey(
                        name: "FK_NewsTag_NewsArticleID",
                        column: x => x.NewsArticleID,
                        principalTable: "NewsArticle",
                        principalColumn: "NewsArticleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsTag_TagID",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryID",
                table: "Category",
                column: "ParentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticle_CategoryID",
                table: "NewsArticle",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticle_CreatedByID",
                table: "NewsArticle",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticle_UpdatedByID",
                table: "NewsArticle",
                column: "UpdatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsTag_TagID",
                table: "NewsTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "UQ__SystemAc__FC770D33B3276155",
                table: "SystemAccount",
                column: "AccountEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsTag");

            migrationBuilder.DropTable(
                name: "NewsArticle");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "SystemAccount");
        }
    }
}
