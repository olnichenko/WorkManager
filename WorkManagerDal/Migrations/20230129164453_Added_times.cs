using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagerDal.Migrations
{
    /// <inheritdoc />
    public partial class Addedtimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeSpents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserCreatedId = table.Column<long>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HoursCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FeatureId = table.Column<long>(type: "INTEGER", nullable: true),
                    BugId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSpents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSpents_Bugs_BugId",
                        column: x => x.BugId,
                        principalTable: "Bugs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeSpents_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeSpents_Users_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSpents_BugId",
                table: "TimeSpents",
                column: "BugId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSpents_FeatureId",
                table: "TimeSpents",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSpents_UserCreatedId",
                table: "TimeSpents",
                column: "UserCreatedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSpents");
        }
    }
}
