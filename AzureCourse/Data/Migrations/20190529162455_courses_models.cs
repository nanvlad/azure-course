using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureCourse.Data.Migrations
{
    public partial class courses_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    CourseId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Title);
                    table.ForeignKey(
                        name: "FK_Module_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clip",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    ModuleTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clip", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Clip_Module_ModuleTitle",
                        column: x => x.ModuleTitle,
                        principalTable: "Module",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clip_ModuleTitle",
                table: "Clip",
                column: "ModuleTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Module_CourseId",
                table: "Module",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clip");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
