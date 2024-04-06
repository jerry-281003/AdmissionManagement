using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionManagement.Migrations
{
    /// <inheritdoc />
    public partial class updateCadidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CadidateStatus",
                table: "Cadidate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Cadidate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PaymentStatus",
                table: "Cadidate",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CadidateStatus",
                table: "Cadidate");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Cadidate");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Cadidate");
        }
    }
}
