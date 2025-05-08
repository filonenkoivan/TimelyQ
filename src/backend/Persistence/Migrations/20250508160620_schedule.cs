using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schedule_ScheduleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ScheduleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LunchTime",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "TimeForEachClient",
                table: "Schedule");

            migrationBuilder.AddColumn<int>(
                name: "WorkDurationTime",
                table: "Schedule",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkEndTime",
                table: "Schedule",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkStartTime",
                table: "Schedule",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ScheduleEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    Time = table.Column<int>(type: "integer", nullable: false),
                    ScheduleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleEntry_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleEntry_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntry_ClientId",
                table: "ScheduleEntry",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntry_ScheduleId",
                table: "ScheduleEntry",
                column: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleEntry");

            migrationBuilder.DropColumn(
                name: "WorkDurationTime",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "WorkEndTime",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "WorkStartTime",
                table: "Schedule");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LunchTime",
                table: "Schedule",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeForEachClient",
                table: "Schedule",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_Users_ScheduleId",
                table: "Users",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Schedule_ScheduleId",
                table: "Users",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id");
        }
    }
}
