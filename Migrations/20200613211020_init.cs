using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PWSZ_Plan_WebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    InstituteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Majors_Institutes_InstituteID",
                        column: x => x.InstituteID,
                        principalTable: "Institutes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    HashCode = table.Column<byte[]>(nullable: true),
                    Permission = table.Column<int>(nullable: false),
                    InstituteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Institutes_InstituteID",
                        column: x => x.InstituteID,
                        principalTable: "Institutes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Specyfications",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    MajorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specyfications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Specyfications_Majors_MajorID",
                        column: x => x.MajorID,
                        principalTable: "Majors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    Start = table.Column<TimeSpan>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Recurrence = table.Column<TimeSpan>(nullable: false),
                    TotalHours = table.Column<int>(nullable: false),
                    SubjectID = table.Column<int>(nullable: false),
                    LecturerID = table.Column<int>(nullable: false),
                    RoomID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Classes_Users_LecturerID",
                        column: x => x.LecturerID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    SpecializationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Years_Specyfications_SpecializationID",
                        column: x => x.SpecializationID,
                        principalTable: "Specyfications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Informations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Message = table.Column<string>(maxLength: 500, nullable: false),
                    AuthorID = table.Column<int>(nullable: false),
                    ClassID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Informations_Users_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Informations_Classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartSessionDate = table.Column<DateTime>(nullable: true),
                    EndSessionDate = table.Column<DateTime>(nullable: true),
                    YearID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Plans_Years_YearID",
                        column: x => x.YearID,
                        principalTable: "Years",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanClass",
                columns: table => new
                {
                    PlanID = table.Column<int>(nullable: false),
                    ClassID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanClass", x => new { x.PlanID, x.ClassID });
                    table.ForeignKey(
                        name: "FK_PlanClass_Classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanClass_Plans_PlanID",
                        column: x => x.PlanID,
                        principalTable: "Plans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LecturerID",
                table: "Classes",
                column: "LecturerID");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_RoomID",
                table: "Classes",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SubjectID",
                table: "Classes",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Informations_AuthorID",
                table: "Informations",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Informations_ClassID",
                table: "Informations",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Majors_InstituteID",
                table: "Majors",
                column: "InstituteID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanClass_ClassID",
                table: "PlanClass",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_YearID",
                table: "Plans",
                column: "YearID");

            migrationBuilder.CreateIndex(
                name: "IX_Specyfications_MajorID",
                table: "Specyfications",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_InstituteID",
                table: "Users",
                column: "InstituteID");

            migrationBuilder.CreateIndex(
                name: "IX_Years_SpecializationID",
                table: "Years",
                column: "SpecializationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Informations");

            migrationBuilder.DropTable(
                name: "PlanClass");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropTable(
                name: "Specyfications");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropTable(
                name: "Institutes");
        }
    }
}
