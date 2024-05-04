using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostModule.Infrastracture.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageAlt",
                table: "Packages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Packages",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageAlt",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Packages");
        }
    }
}
