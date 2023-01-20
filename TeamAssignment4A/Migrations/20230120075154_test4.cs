using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamAssignment4A.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Candidates_CandidateNumber",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExam_Candidates_CandidateNumber",
                table: "CandidateExam");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExam_Exams_ExamId",
                table: "CandidateExam");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExam_Score_ScoreId",
                table: "CandidateExam");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStem_Exams_ExamsId",
                table: "ExamStem");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStem_Stems_StemsId",
                table: "ExamStem");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamTopic_Exams_ExamId",
                table: "ExamTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamTopic_Topics_TopicId",
                table: "ExamTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_ExamStem_ExamStemId",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_ExamTopic_ExamTopicId",
                table: "Score");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Score",
                table: "Score");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamTopic",
                table: "ExamTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamStem",
                table: "ExamStem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateExam",
                table: "CandidateExam");

            migrationBuilder.RenameTable(
                name: "Score",
                newName: "Scores");

            migrationBuilder.RenameTable(
                name: "ExamTopic",
                newName: "ExamTopics");

            migrationBuilder.RenameTable(
                name: "ExamStem",
                newName: "ExamStems");

            migrationBuilder.RenameTable(
                name: "CandidateExam",
                newName: "CandidateCertificates");

            migrationBuilder.RenameColumn(
                name: "CandidateNumber",
                table: "Candidates",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CandidateNumber",
                table: "AspNetUsers",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CandidateNumber",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_Score_ExamTopicId",
                table: "Scores",
                newName: "IX_Scores_ExamTopicId");

            migrationBuilder.RenameIndex(
                name: "IX_Score_ExamStemId",
                table: "Scores",
                newName: "IX_Scores_ExamStemId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamTopic_TopicId",
                table: "ExamTopics",
                newName: "IX_ExamTopics_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamTopic_ExamId",
                table: "ExamTopics",
                newName: "IX_ExamTopics_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStem_StemsId",
                table: "ExamStems",
                newName: "IX_ExamStems_StemsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStem_ExamsId",
                table: "ExamStems",
                newName: "IX_ExamStems_ExamsId");

            migrationBuilder.RenameColumn(
                name: "CandidateNumber",
                table: "CandidateCertificates",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateExam_ScoreId",
                table: "CandidateCertificates",
                newName: "IX_CandidateCertificates_ScoreId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateExam_ExamId",
                table: "CandidateCertificates",
                newName: "IX_CandidateCertificates_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateExam_CandidateNumber",
                table: "CandidateCertificates",
                newName: "IX_CandidateCertificates_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scores",
                table: "Scores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamTopics",
                table: "ExamTopics",
                column: "ExamTopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamStems",
                table: "ExamStems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateCertificates",
                table: "CandidateCertificates",
                column: "CandidateExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Candidates_CandidateId",
                table: "AspNetUsers",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateCertificates_Candidates_CandidateId",
                table: "CandidateCertificates",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateCertificates_Exams_ExamId",
                table: "CandidateCertificates",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateCertificates_Scores_ScoreId",
                table: "CandidateCertificates",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTopics_Exams_ExamId",
                table: "ExamTopics",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTopics_Topics_TopicId",
                table: "ExamTopics",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_ExamStems_ExamStemId",
                table: "Scores",
                column: "ExamStemId",
                principalTable: "ExamStems",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_ExamTopics_ExamTopicId",
                table: "Scores",
                column: "ExamTopicId",
                principalTable: "ExamTopics",
                principalColumn: "ExamTopicId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Candidates_CandidateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateCertificates_Candidates_CandidateId",
                table: "CandidateCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateCertificates_Exams_ExamId",
                table: "CandidateCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateCertificates_Scores_ScoreId",
                table: "CandidateCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStems_Exams_ExamsId",
                table: "ExamStems");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStems_Stems_StemsId",
                table: "ExamStems");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamTopics_Exams_ExamId",
                table: "ExamTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamTopics_Topics_TopicId",
                table: "ExamTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_ExamStems_ExamStemId",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_ExamTopics_ExamTopicId",
                table: "Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scores",
                table: "Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamTopics",
                table: "ExamTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamStems",
                table: "ExamStems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateCertificates",
                table: "CandidateCertificates");

            migrationBuilder.RenameTable(
                name: "Scores",
                newName: "Score");

            migrationBuilder.RenameTable(
                name: "ExamTopics",
                newName: "ExamTopic");

            migrationBuilder.RenameTable(
                name: "ExamStems",
                newName: "ExamStem");

            migrationBuilder.RenameTable(
                name: "CandidateCertificates",
                newName: "CandidateExam");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Candidates",
                newName: "CandidateNumber");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "AspNetUsers",
                newName: "CandidateNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CandidateId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CandidateNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Scores_ExamTopicId",
                table: "Score",
                newName: "IX_Score_ExamTopicId");

            migrationBuilder.RenameIndex(
                name: "IX_Scores_ExamStemId",
                table: "Score",
                newName: "IX_Score_ExamStemId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamTopics_TopicId",
                table: "ExamTopic",
                newName: "IX_ExamTopic_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamTopics_ExamId",
                table: "ExamTopic",
                newName: "IX_ExamTopic_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStems_StemsId",
                table: "ExamStem",
                newName: "IX_ExamStem_StemsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStems_ExamsId",
                table: "ExamStem",
                newName: "IX_ExamStem_ExamsId");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "CandidateExam",
                newName: "CandidateNumber");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateCertificates_ScoreId",
                table: "CandidateExam",
                newName: "IX_CandidateExam_ScoreId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateCertificates_ExamId",
                table: "CandidateExam",
                newName: "IX_CandidateExam_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateCertificates_CandidateId",
                table: "CandidateExam",
                newName: "IX_CandidateExam_CandidateNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Score",
                table: "Score",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamTopic",
                table: "ExamTopic",
                column: "ExamTopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamStem",
                table: "ExamStem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateExam",
                table: "CandidateExam",
                column: "CandidateExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Candidates_CandidateNumber",
                table: "AspNetUsers",
                column: "CandidateNumber",
                principalTable: "Candidates",
                principalColumn: "CandidateNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExam_Candidates_CandidateNumber",
                table: "CandidateExam",
                column: "CandidateNumber",
                principalTable: "Candidates",
                principalColumn: "CandidateNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExam_Exams_ExamId",
                table: "CandidateExam",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExam_Score_ScoreId",
                table: "CandidateExam",
                column: "ScoreId",
                principalTable: "Score",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStem_Exams_ExamsId",
                table: "ExamStem",
                column: "ExamsId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStem_Stems_StemsId",
                table: "ExamStem",
                column: "StemsId",
                principalTable: "Stems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTopic_Exams_ExamId",
                table: "ExamTopic",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTopic_Topics_TopicId",
                table: "ExamTopic",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_ExamStem_ExamStemId",
                table: "Score",
                column: "ExamStemId",
                principalTable: "ExamStem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_ExamTopic_ExamTopicId",
                table: "Score",
                column: "ExamTopicId",
                principalTable: "ExamTopic",
                principalColumn: "ExamTopicId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
