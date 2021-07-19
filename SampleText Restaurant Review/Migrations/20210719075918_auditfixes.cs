using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleText_Restaurant_Review.Migrations
{
    public partial class auditfixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditRecord_Restaurant_RestaurantID",
                table: "AuditRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditRecord_Reviews_ReviewID",
                table: "AuditRecord");

            migrationBuilder.DropIndex(
                name: "IX_AuditRecord_RestaurantID",
                table: "AuditRecord");

            migrationBuilder.DropIndex(
                name: "IX_AuditRecord_ReviewID",
                table: "AuditRecord");

            migrationBuilder.DropColumn(
                name: "RestaurantID",
                table: "AuditRecord");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewID",
                table: "AuditRecord",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RestaurantName",
                table: "AuditRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantName",
                table: "AuditRecord");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewID",
                table: "AuditRecord",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RestaurantID",
                table: "AuditRecord",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecord_RestaurantID",
                table: "AuditRecord",
                column: "RestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecord_ReviewID",
                table: "AuditRecord",
                column: "ReviewID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditRecord_Restaurant_RestaurantID",
                table: "AuditRecord",
                column: "RestaurantID",
                principalTable: "Restaurant",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditRecord_Reviews_ReviewID",
                table: "AuditRecord",
                column: "ReviewID",
                principalTable: "Reviews",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
