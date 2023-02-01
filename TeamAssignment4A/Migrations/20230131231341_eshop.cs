using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamAssignment4A.Migrations
{
    public partial class eshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExaminationDate",
                table: "Exams",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CandidateId",
                table: "Exams",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Candidates_CandidateId",
                table: "Exams",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Candidates_CandidateId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_CandidateId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Exams");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExaminationDate",
                table: "Exams",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);
        }
    }
}
