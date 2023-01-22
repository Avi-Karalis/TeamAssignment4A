using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamAssignment4A.Migrations
{
    public partial class examworks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamStems_Exams_ExamsId",
                table: "ExamStems");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStems_Stems_StemsId",
                table: "ExamStems");

            migrationBuilder.RenameColumn(
                name: "StemsId",
                table: "ExamStems",
                newName: "StemId");

            migrationBuilder.RenameColumn(
                name: "ExamsId",
                table: "ExamStems",
                newName: "ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStems_StemsId",
                table: "ExamStems",
                newName: "IX_ExamStems_StemId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStems_ExamsId",
                table: "ExamStems",
                newName: "IX_ExamStems_ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStems_Exams_ExamId",
                table: "ExamStems",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStems_Stems_StemId",
                table: "ExamStems",
                column: "StemId",
                principalTable: "Stems",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamStems_Exams_ExamId",
                table: "ExamStems");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStems_Stems_StemId",
                table: "ExamStems");

            migrationBuilder.RenameColumn(
                name: "StemId",
                table: "ExamStems",
                newName: "StemsId");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "ExamStems",
                newName: "ExamsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStems_StemId",
                table: "ExamStems",
                newName: "IX_ExamStems_StemsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStems_ExamId",
                table: "ExamStems",
                newName: "IX_ExamStems_ExamsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStems_Exams_ExamsId",
                table: "ExamStems",
                column: "ExamsId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStems_Stems_StemsId",
                table: "ExamStems",
                column: "StemsId",
                principalTable: "Stems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
