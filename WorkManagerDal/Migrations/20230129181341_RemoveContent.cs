using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagerDal.Migrations
{
    /// <inheritdoc />
    public partial class RemoveContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Projects",
                type: "TEXT",
                nullable: true);
        }
    }
}
