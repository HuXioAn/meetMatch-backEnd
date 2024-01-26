using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mmApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "timeTables",
                columns: table => new
                {
                    tableVisitToken = table.Column<string>(type: "TEXT", nullable: false),
                    meetingName = table.Column<string>(type: "TEXT", nullable: false),
                    dateSelection = table.Column<string>(type: "TEXT", nullable: false),
                    timeRange = table.Column<string>(type: "TEXT", nullable: false),
                    maxCollaborator = table.Column<int>(type: "INTEGER", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    state = table.Column<int>(type: "INTEGER", nullable: false),
                    tableManageToken = table.Column<string>(type: "TEXT", nullable: false),
                    existingSelection = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeTables", x => x.tableVisitToken);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "timeTables");
        }
    }
}
