using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionManagement.Migrations
{
    /// <inheritdoc />
    public partial class cadidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "SubjectCombination",
            table: "Cadidate",
            nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "SubjectCombination",
            table: "Cadidate");
        }
    }
}
