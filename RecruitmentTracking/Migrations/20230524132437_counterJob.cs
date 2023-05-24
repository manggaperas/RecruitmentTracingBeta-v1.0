using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentTracking.Migrations
{
    /// <inheritdoc />
    public partial class counterJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusInJob",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "candidateCount",
                table: "Jobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "candidateCount",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "StatusInJob",
                table: "Candidates",
                type: "TEXT",
                nullable: true);
        }
    }
}
