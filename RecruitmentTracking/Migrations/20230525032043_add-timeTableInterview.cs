using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentTracking.Migrations
{
    /// <inheritdoc />
    public partial class addtimeTableInterview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateHRInterview",
                table: "UserJobs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUserInterview",
                table: "UserJobs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SendEmailStatus",
                table: "UserJobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeHRInterview",
                table: "UserJobs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeUserInterview",
                table: "UserJobs",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateHRInterview",
                table: "UserJobs");

            migrationBuilder.DropColumn(
                name: "DateUserInterview",
                table: "UserJobs");

            migrationBuilder.DropColumn(
                name: "SendEmailStatus",
                table: "UserJobs");

            migrationBuilder.DropColumn(
                name: "TimeHRInterview",
                table: "UserJobs");

            migrationBuilder.DropColumn(
                name: "TimeUserInterview",
                table: "UserJobs");
        }
    }
}
