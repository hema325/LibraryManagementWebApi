using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingAuthorGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_GenderId",
                table: "Authors",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Genders_GenderId",
                table: "Authors",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Genders_GenderId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_GenderId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Authors");
        }
    }
}
