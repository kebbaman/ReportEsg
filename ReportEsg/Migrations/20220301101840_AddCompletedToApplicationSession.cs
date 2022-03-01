using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportEsg.Migrations
{
    public partial class AddCompletedToApplicationSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "ApplicationSessions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "ApplicationSessions");
        }
    }
}
