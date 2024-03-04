using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BS.ReceiptAnalyzer.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialAnalysisTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalysisTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Progression = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Results = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FailReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalysisTaskStep",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Success = table.Column<bool>(type: "bit", nullable: false),
                    NotificationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AnalysisTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisTaskStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisTaskStep_AnalysisTask_AnalysisTaskId",
                        column: x => x.AnalysisTaskId,
                        principalTable: "AnalysisTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisTask_ImageHash",
                table: "AnalysisTask",
                column: "ImageHash");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisTaskStep_AnalysisTaskId",
                table: "AnalysisTaskStep",
                column: "AnalysisTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisTaskStep");

            migrationBuilder.DropTable(
                name: "AnalysisTask");
        }
    }
}
