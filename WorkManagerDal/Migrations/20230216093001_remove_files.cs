using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagerDal.Migrations
{
    /// <inheritdoc />
    public partial class removefiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadedFiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BugId = table.Column<long>(type: "INTEGER", nullable: true),
                    CommentId = table.Column<long>(type: "INTEGER", nullable: true),
                    FeatureId = table.Column<long>(type: "INTEGER", nullable: true),
                    NoteId = table.Column<long>(type: "INTEGER", nullable: true),
                    ProjectId = table.Column<long>(type: "INTEGER", nullable: true),
                    UserCreatedId = table.Column<long>(type: "INTEGER", nullable: true),
                    VersionId = table.Column<long>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    FileType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Bugs_BugId",
                        column: x => x.BugId,
                        principalTable: "Bugs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Users_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Versions_VersionId",
                        column: x => x.VersionId,
                        principalTable: "Versions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_BugId",
                table: "UploadedFiles",
                column: "BugId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_CommentId",
                table: "UploadedFiles",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_FeatureId",
                table: "UploadedFiles",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_NoteId",
                table: "UploadedFiles",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_ProjectId",
                table: "UploadedFiles",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_UserCreatedId",
                table: "UploadedFiles",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_VersionId",
                table: "UploadedFiles",
                column: "VersionId");
        }
    }
}
