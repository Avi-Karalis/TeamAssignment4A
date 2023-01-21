using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamAssignment4A.Migrations
{
    public partial class StemTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stems_Topics_TopicId",
                table: "Stems");

            migrationBuilder.RenameColumn(
                name: "TopicId",
                table: "Stems",
                newName: "TopicID");

            migrationBuilder.RenameIndex(
                name: "IX_Stems_TopicId",
                table: "Stems",
                newName: "IX_Stems_TopicID");

            migrationBuilder.CreateTable(
                name: "ExamTopics",
                columns: table => new
                {
                    ExamTopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTopics", x => x.ExamTopicId);
                    table.ForeignKey(
                        name: "FK_ExamTopics_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamTopics_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamTopics_ExamId",
                table: "ExamTopics",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTopics_TopicId",
                table: "ExamTopics",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stems_Topics_TopicID",
                table: "Stems",
                column: "TopicID",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stems_Topics_TopicID",
                table: "Stems");

            migrationBuilder.DropTable(
                name: "ExamTopics");

            migrationBuilder.RenameColumn(
                name: "TopicID",
                table: "Stems",
                newName: "TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_Stems_TopicID",
                table: "Stems",
                newName: "IX_Stems_TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stems_Topics_TopicId",
                table: "Stems",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
