using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostModule.Infrastracture.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddApiDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiDescription",
                table: "PostSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiDescription",
                table: "PostSettings");
        }
    }
}
