using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionManagement.Migrations
{
    /// <inheritdoc />
    public partial class updateCadidate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Cadidate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Cadidate");
        }
    }
}
