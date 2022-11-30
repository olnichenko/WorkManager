using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagerDal.Migrations
{
    /// <inheritdoc />
    public partial class Addedprojectmodels666sdfasdfsaffffffffff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserCreatedId",
                table: "Projects");

            migrationBuilder.AlterColumn<long>(
                name: "UserCreatedId",
                table: "Projects",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserCreatedId",
                table: "Projects",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserCreatedId",
                table: "Projects");

            migrationBuilder.AlterColumn<long>(
                name: "UserCreatedId",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserCreatedId",
                table: "Projects",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
