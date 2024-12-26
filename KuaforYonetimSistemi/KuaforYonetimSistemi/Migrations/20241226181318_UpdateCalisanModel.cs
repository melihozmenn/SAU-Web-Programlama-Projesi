using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforYonetimSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCalisanModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salon",
                table: "Islems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Islems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salon",
                table: "Islems");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Islems");
        }
    }
}
