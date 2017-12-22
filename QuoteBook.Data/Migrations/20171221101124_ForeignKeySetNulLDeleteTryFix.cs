using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QuoteBook.Data.Migrations
{
    public partial class ForeignKeySetNulLDeleteTryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Inspirators_InspiratorId",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Inspirators_InspiratorId",
                table: "Posts",
                column: "InspiratorId",
                principalTable: "Inspirators",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Inspirators_InspiratorId",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Inspirators_InspiratorId",
                table: "Posts",
                column: "InspiratorId",
                principalTable: "Inspirators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
