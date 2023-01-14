using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamAssignment4A.Migrations
{
    /// <inheritdoc />
    public partial class gamame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NativeLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryOfResidence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "Date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandLineNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoIdType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoIdDate = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateNumber);
                });

            migrationBuilder.CreateTable(
                name: "Stems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPossibleMarks = table.Column<int>(type: "int", nullable: false),
                    CertificateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StemTopic",
                columns: table => new
                {
                    StemsId = table.Column<int>(type: "int", nullable: false),
                    TopicsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StemTopic", x => new { x.StemsId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_StemTopic_Stems_StemsId",
                        column: x => x.StemsId,
                        principalTable: "Stems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StemTopic_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleOfCertificate = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PassingGrade = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    MaximumScore = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certificates_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamStem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamsId = table.Column<int>(type: "int", nullable: false),
                    StemsId = table.Column<int>(type: "int", nullable: false),
                    SubmittedAnswer = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamStem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamStem_Exams_ExamsId",
                        column: x => x.ExamsId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamStem_Stems_StemsId",
                        column: x => x.StemsId,
                        principalTable: "Stems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamTopic",
                columns: table => new
                {
                    ExamTopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamsId = table.Column<int>(type: "int", nullable: false),
                    TopicsId = table.Column<int>(type: "int", nullable: false),
                    CertificateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTopic", x => x.ExamTopicId);
                    table.ForeignKey(
                        name: "FK_ExamTopic_Certificates_CertificateId",
                        column: x => x.CertificateId,
                        principalTable: "Certificates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExamTopic_Exams_ExamsId",
                        column: x => x.ExamsId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamTopic_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamTopicsExamTopicId = table.Column<int>(type: "int", nullable: false),
                    ExamStemsId = table.Column<int>(type: "int", nullable: false),
                    ScorePerTopic = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_ExamStem_ExamStemsId",
                        column: x => x.ExamStemsId,
                        principalTable: "ExamStem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Score_ExamTopic_ExamTopicsExamTopicId",
                        column: x => x.ExamTopicsExamTopicId,
                        principalTable: "ExamTopic",
                        principalColumn: "ExamTopicId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CandidateExam",
                columns: table => new
                {
                    CandidateExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentTestCode = table.Column<int>(type: "int", nullable: false),
                    ExaminationDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ScoreReportDate = table.Column<DateTime>(type: "Date", nullable: false),
                    CandidateScore = table.Column<int>(type: "int", nullable: false),
                    AssessmentResultLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentageScore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidatesCandidateNumber = table.Column<int>(type: "int", nullable: false),
                    ExamsId = table.Column<int>(type: "int", nullable: false),
                    ScoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateExam", x => x.CandidateExamId);
                    table.ForeignKey(
                        name: "FK_CandidateExam_Candidates_CandidatesCandidateNumber",
                        column: x => x.CandidatesCandidateNumber,
                        principalTable: "Candidates",
                        principalColumn: "CandidateNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateExam_Exams_ExamsId",
                        column: x => x.ExamsId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateExam_Score_ScoresId",
                        column: x => x.ScoresId,
                        principalTable: "Score",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateExam_CandidatesCandidateNumber",
                table: "CandidateExam",
                column: "CandidatesCandidateNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateExam_ExamsId",
                table: "CandidateExam",
                column: "ExamsId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateExam_ScoresId",
                table: "CandidateExam",
                column: "ScoresId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ExamId",
                table: "Certificates",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_TopicId",
                table: "Certificates",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_TopicId",
                table: "Exams",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamStem_ExamsId",
                table: "ExamStem",
                column: "ExamsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamStem_StemsId",
                table: "ExamStem",
                column: "StemsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTopic_CertificateId",
                table: "ExamTopic",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTopic_ExamsId",
                table: "ExamTopic",
                column: "ExamsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTopic_TopicsId",
                table: "ExamTopic",
                column: "TopicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_ExamStemsId",
                table: "Score",
                column: "ExamStemsId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_ExamTopicsExamTopicId",
                table: "Score",
                column: "ExamTopicsExamTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_StemTopic_TopicsId",
                table: "StemTopic",
                column: "TopicsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateExam");

            migrationBuilder.DropTable(
                name: "StemTopic");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "ExamStem");

            migrationBuilder.DropTable(
                name: "ExamTopic");

            migrationBuilder.DropTable(
                name: "Stems");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
