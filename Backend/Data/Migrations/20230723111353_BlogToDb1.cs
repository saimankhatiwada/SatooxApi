using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class BlogToDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AdminId",
                table: "Blogs",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AdminUsers_AdminId",
                table: "Blogs",
                column: "AdminId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AdminUsers_AdminId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AdminId",
                table: "Blogs");
        }
    }
}
