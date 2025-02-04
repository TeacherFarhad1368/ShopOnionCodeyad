using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderDiscountTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DiscountTitle",
                table: "OrderSellers",
                type: "nvarchar(355)",
                maxLength: 355,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DiscountTitle",
                table: "Orders",
                type: "nvarchar(355)",
                maxLength: 355,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountTitle",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "DiscountTitle",
                table: "OrderSellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(355)",
                oldMaxLength: 355,
                oldNullable: true);
        }
    }
}
