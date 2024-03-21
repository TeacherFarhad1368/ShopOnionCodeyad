using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Site.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Baners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    ImageAlt = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    ImageAlt = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(355)", maxLength: 355, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    ImageAlt = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instagram = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    Telegram = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    Youtube = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    LogoName = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: true),
                    LogoAlt = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: true),
                    FavIcon = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: true),
                    Enamad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SamanDehi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Android = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    IOS = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    FooterDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Email1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ContactDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    ImageAlt = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentId",
                table: "Menus",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Baners");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "SiteServices");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
