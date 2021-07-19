using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleText_Restaurant_Review.Migrations
{
    public partial class auditlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditRecord",
                columns: table => new
                {
                    Audit_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditActionType = table.Column<string>(nullable: true),
                    FullNameId = table.Column<string>(nullable: true),
                    DateTimeStamp = table.Column<DateTime>(nullable: false),
                    RestaurantID = table.Column<int>(nullable: true),
                    ReviewID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditRecord", x => x.Audit_ID);
                    table.ForeignKey(
                        name: "FK_AuditRecord_AspNetUsers_FullNameId",
                        column: x => x.FullNameId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditRecord_Restaurant_RestaurantID",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditRecord_Reviews_ReviewID",
                        column: x => x.ReviewID,
                        principalTable: "Reviews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecord_FullNameId",
                table: "AuditRecord",
                column: "FullNameId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecord_RestaurantID",
                table: "AuditRecord",
                column: "RestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecord_ReviewID",
                table: "AuditRecord",
                column: "ReviewID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditRecord");
        }
    }
}
