using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagerDal.Migrations
{
    /// <inheritdoc />
    public partial class Addeddelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Users_UserCreatedId",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Users_UserCreatedId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserCreatedId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Users_UserCreatedId",
                table: "Versions");

            migrationBuilder.AlterColumn<long>(
                name: "UserCreatedId",
                table: "Versions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Versions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserCreatedId",
                table: "Notes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Notes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<long>(
                name: "UserCreatedId",
                table: "Features",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Features",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Features",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserCreatedId",
                table: "Bugs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Bugs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Bugs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Users_UserCreatedId",
                table: "Bugs",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Users_UserCreatedId",
                table: "Features",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserCreatedId",
                table: "Notes",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Users_UserCreatedId",
                table: "Versions",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Users_UserCreatedId",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Users_UserCreatedId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserCreatedId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Users_UserCreatedId",
                table: "Versions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Bugs");

            migrationBuilder.AlterColumn<long>(
                name: "UserCreatedId",
                table: "Versions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Versions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserCreatedId",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Notes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserCreatedId",
                table: "Features",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Features",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserCreatedId",
                table: "Bugs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Bugs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Users_UserCreatedId",
                table: "Bugs",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Users_UserCreatedId",
                table: "Features",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserCreatedId",
                table: "Notes",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Users_UserCreatedId",
                table: "Versions",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
