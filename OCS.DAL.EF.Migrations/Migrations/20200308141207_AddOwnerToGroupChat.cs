using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCS.DAL.EF.Migrations.Migrations
{
    public partial class AddOwnerToGroupChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                schema: "user",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSeenAt",
                schema: "user",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                schema: "chat",
                table: "GroupChats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_OwnerId",
                schema: "chat",
                table: "GroupChats",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChats_Users_OwnerId",
                schema: "chat",
                table: "GroupChats",
                column: "OwnerId",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChats_Users_OwnerId",
                schema: "chat",
                table: "GroupChats");

            migrationBuilder.DropIndex(
                name: "IX_GroupChats_OwnerId",
                schema: "chat",
                table: "GroupChats");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastSeenAt",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "chat",
                table: "GroupChats");
        }
    }
}
