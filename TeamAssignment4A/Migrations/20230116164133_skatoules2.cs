using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamAssignment4A.Migrations
{
    public partial class skatoules2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateNumber",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CandidateNumber",
                table: "AspNetUsers",
                column: "CandidateNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Candidates_CandidateNumber",
                table: "AspNetUsers",
                column: "CandidateNumber",
                principalTable: "Candidates",
                principalColumn: "CandidateNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Candidates_CandidateNumber",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CandidateNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CandidateNumber",
                table: "AspNetUsers");
        }
    }
}
