using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreationDate", "Title" },
                values: new object[] { 1, "This is the first post", new DateTime(2023, 11, 25, 16, 25, 56, 108, DateTimeKind.Local).AddTicks(510), "First Post" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreationDate", "Title" },
                values: new object[] { 2, "This is the second post", new DateTime(2023, 11, 25, 16, 25, 56, 108, DateTimeKind.Local).AddTicks(520), "Second Post" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreationDate", "Title" },
                values: new object[] { 3, "This is the third post", new DateTime(2023, 11, 25, 16, 25, 56, 108, DateTimeKind.Local).AddTicks(520), "Third Post" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "PostId", "Title" },
                values: new object[] { 1, "This is the first comment", new DateTime(2023, 11, 25, 16, 25, 56, 108, DateTimeKind.Local).AddTicks(580), 1, "First Comment" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "PostId", "Title" },
                values: new object[] { 2, "This is the second comment", new DateTime(2023, 11, 25, 16, 25, 56, 108, DateTimeKind.Local).AddTicks(580), 1, "Second Comment" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "PostId", "Title" },
                values: new object[] { 3, "This is the first comment", new DateTime(2023, 11, 25, 16, 25, 56, 108, DateTimeKind.Local).AddTicks(580), 2, "First Comment" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "PostId", "Title" },
                values: new object[] { 4, "This is the second comment", new DateTime(2023, 11, 25, 16, 25, 56, 108, DateTimeKind.Local).AddTicks(590), 2, "Second Comment" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "PostId", "Title" },
                values: new object[] { 5, "This is the first comment", new DateTime(2023, 11, 25, 16, 25, 56, 108, DateTimeKind.Local).AddTicks(590), 3, "First Comment" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "PostId", "Title" },
                values: new object[] { 6, "This is the second comment", new DateTime(2023, 11, 25, 16, 25, 56, 108, DateTimeKind.Local).AddTicks(590), 3, "Second Comment" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
