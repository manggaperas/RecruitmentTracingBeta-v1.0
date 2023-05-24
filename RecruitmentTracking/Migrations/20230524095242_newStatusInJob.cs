using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentTracking.Migrations
{
    /// <inheritdoc />
    public partial class newStatusInJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusInJob",
                table: "UserJobs",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusInJob",
                table: "UserJobs");
        }
    }
}
