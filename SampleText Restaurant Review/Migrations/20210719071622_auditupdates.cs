using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleText_Restaurant_Review.Migrations
{
    public partial class auditupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditRecord_AspNetUsers_FullNameId",
                table: "AuditRecord");

            migrationBuilder.DropIndex(
                name: "IX_AuditRecord_FullNameId",
                table: "AuditRecord");

            migrationBuilder.DropColumn(
                name: "FullNameId",
                table: "AuditRecord");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AuditRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AuditRecord");

            migrationBuilder.AddColumn<string>(
                name: "FullNameId",
                table: "AuditRecord",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecord_FullNameId",
                table: "AuditRecord",
                column: "FullNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditRecord_AspNetUsers_FullNameId",
                table: "AuditRecord",
                column: "FullNameId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
