using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamAssignment4A.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    PhotoIdDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateNumber);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleOfCertificate = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PassingGrade = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    MaximumScore = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentTestCode = table.Column<int>(type: "int", nullable: false),
                    ExaminationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoreReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CandidateScore = table.Column<int>(type: "int", nullable: false),
                    AssessmentResultLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentageScore = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
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
                    CorrectAnswer = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    StemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stems_Stems_StemId",
                        column: x => x.StemId,
                        principalTable: "Stems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateCertificate",
                columns: table => new
                {
                    CandidatesCandidateNumber = table.Column<int>(type: "int", nullable: false),
                    CertificatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCertificate", x => new { x.CandidatesCandidateNumber, x.CertificatesId });
                    table.ForeignKey(
                        name: "FK_CandidateCertificate_Candidates_CandidatesCandidateNumber",
                        column: x => x.CandidatesCandidateNumber,
                        principalTable: "Candidates",
                        principalColumn: "CandidateNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateCertificate_Certificates_CertificatesId",
                        column: x => x.CertificatesId,
                        principalTable: "Certificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPossibleMarks = table.Column<int>(type: "int", nullable: false),
                    CertificateID = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Topics_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExamStem",
                columns: table => new
                {
                    ExamsId = table.Column<int>(type: "int", nullable: false),
                    StemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamStem", x => new { x.ExamsId, x.StemsId });
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
                name: "CertificateTopic",
                columns: table => new
                {
                    CertificatesId = table.Column<int>(type: "int", nullable: false),
                    TopicsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateTopic", x => new { x.CertificatesId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_CertificateTopic_Certificates_CertificatesId",
                        column: x => x.CertificatesId,
                        principalTable: "Certificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertificateTopic_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCertificate_CertificatesId",
                table: "CandidateCertificate",
                column: "CertificatesId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateTopic_TopicsId",
                table: "CertificateTopic",
                column: "TopicsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamStem_StemsId",
                table: "ExamStem",
                column: "StemsId");

            migrationBuilder.CreateIndex(
                name: "IX_Stems_StemId",
                table: "Stems",
                column: "StemId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ExamId",
                table: "Topics",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_TopicId",
                table: "Topics",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateCertificate");

            migrationBuilder.DropTable(
                name: "CertificateTopic");

            migrationBuilder.DropTable(
                name: "ExamStem");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Stems");

            migrationBuilder.DropTable(
                name: "Exams");
        }
    }
}
