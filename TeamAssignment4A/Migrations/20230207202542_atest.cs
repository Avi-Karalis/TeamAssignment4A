using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamAssignment4A.Migrations
{
    public partial class atest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserID",
                table: "Candidates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_IdentityUserID",
                table: "Candidates",
                column: "IdentityUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_IdentityUserID",
                table: "Candidates",
                column: "IdentityUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_IdentityUserID",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_IdentityUserID",
                table: "Candidates");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserID",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
