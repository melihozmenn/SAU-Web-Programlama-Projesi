using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforYonetimSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddOnayliToRandevu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Onayli",
                table: "Randevus",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Onayli",
                table: "Randevus");
        }
    }
}
