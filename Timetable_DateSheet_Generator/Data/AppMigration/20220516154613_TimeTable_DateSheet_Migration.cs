using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Timetable_DateSheet_Generator.Data.AppMigration
{
    public partial class TimeTable_DateSheet_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Institutes",
                columns: table => new
                {
                    InstituteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InstituteName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutes", x => x.InstituteID);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    SemesterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SemesterType = table.Column<int>(type: "int", nullable: false),
                    SemesterYear = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.SemesterID);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    TimeID = table.Column<int>(type: "int", nullable: false),
                    TimeWeekDay = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<int>(type: "int", nullable: false),
                    FinishTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.TimeID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InstituteID = table.Column<int>(type: "int", nullable: false),
                    BuildingName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingID);
                    table.ForeignKey(
                        name: "FK_Buildings_Institutes_InstituteID",
                        column: x => x.InstituteID,
                        principalTable: "Institutes",
                        principalColumn: "InstituteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InstituteID = table.Column<int>(type: "int", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_Departments_Institutes_InstituteID",
                        column: x => x.InstituteID,
                        principalTable: "Institutes",
                        principalColumn: "InstituteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeTables",
                columns: table => new
                {
                    TimeTableID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InstituteID = table.Column<int>(type: "int", nullable: false),
                    SemesterID = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTables", x => x.TimeTableID);
                    table.ForeignKey(
                        name: "FK_TimeTables_Institutes_InstituteID",
                        column: x => x.InstituteID,
                        principalTable: "Institutes",
                        principalColumn: "InstituteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeTables_Semesters_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "SemesterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuildingID = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<int>(type: "int", nullable: false),
                    SeatingCapacity = table.Column<int>(type: "int", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Rooms_Buildings_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Buildings",
                        principalColumn: "BuildingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyMembers",
                columns: table => new
                {
                    FacultyMemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyMembers", x => x.FacultyMemberID);
                    table.ForeignKey(
                        name: "FK_FacultyMembers_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    ProgramID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramID);
                    table.ForeignKey(
                        name: "FK_Programs_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    TimeSlotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeTableID = table.Column<int>(type: "int", nullable: false),
                    TimeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.TimeSlotID);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Times_TimeID",
                        column: x => x.TimeID,
                        principalTable: "Times",
                        principalColumn: "TimeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSlots_TimeTables_TimeTableID",
                        column: x => x.TimeTableID,
                        principalTable: "TimeTables",
                        principalColumn: "TimeTableID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomAvailibilities",
                columns: table => new
                {
                    RoomAvailibilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    TimeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAvailibilities", x => x.RoomAvailibilityID);
                    table.ForeignKey(
                        name: "FK_RoomAvailibilities_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomAvailibilities_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RoomAvailibilities_Times_TimeID",
                        column: x => x.TimeID,
                        principalTable: "Times",
                        principalColumn: "TimeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyMemberAvailabilities",
                columns: table => new
                {
                    FacultyMemberAvailabilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacultyMemberID = table.Column<int>(type: "int", nullable: false),
                    TimeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyMemberAvailabilities", x => x.FacultyMemberAvailabilityID);
                    table.ForeignKey(
                        name: "FK_FacultyMemberAvailabilities_FacultyMembers_FacultyMemberID",
                        column: x => x.FacultyMemberID,
                        principalTable: "FacultyMembers",
                        principalColumn: "FacultyMemberID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyMemberAvailabilities_Times_TimeID",
                        column: x => x.TimeID,
                        principalTable: "Times",
                        principalColumn: "TimeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferedCourses",
                columns: table => new
                {
                    OfferedCourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacultyMemberID = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    SemesterID = table.Column<int>(type: "int", nullable: false),
                    OfferedCourseTitle = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    OfferedCourseCreditHours = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    OfferedCourseContactHours = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    OfferedCourseCategory = table.Column<int>(type: "int", nullable: false),
                    OfferedCourseSpecial = table.Column<int>(type: "int", nullable: false),
                    OfferedCourseSection = table.Column<string>(type: "char(1)", nullable: false),
                    OfferedCourseSemesterNo = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferedCourses", x => x.OfferedCourseID);
                    table.ForeignKey(
                        name: "FK_OfferedCourses_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferedCourses_FacultyMembers_FacultyMemberID",
                        column: x => x.FacultyMemberID,
                        principalTable: "FacultyMembers",
                        principalColumn: "FacultyMemberID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OfferedCourses_Programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "Programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OfferedCourses_Semesters_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "SemesterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramRegularTimings",
                columns: table => new
                {
                    ProgramRegularTimingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    TimeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramRegularTimings", x => x.ProgramRegularTimingID);
                    table.ForeignKey(
                        name: "FK_ProgramRegularTimings_Programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "Programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramRegularTimings_Times_TimeID",
                        column: x => x.TimeID,
                        principalTable: "Times",
                        principalColumn: "TimeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramSpecialTimings",
                columns: table => new
                {
                    ProgramSpecialTimingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    TimeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSpecialTimings", x => x.ProgramSpecialTimingID);
                    table.ForeignKey(
                        name: "FK_ProgramSpecialTimings_Programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "Programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramSpecialTimings_Times_TimeID",
                        column: x => x.TimeID,
                        principalTable: "Times",
                        principalColumn: "TimeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProgarmID = table.Column<int>(type: "int", nullable: false),
                    StudentEnrollment = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                    table.UniqueConstraint("AK_Students_StudentEnrollment", x => x.StudentEnrollment);
                    table.ForeignKey(
                        name: "FK_Students_Programs_ProgarmID",
                        column: x => x.ProgarmID,
                        principalTable: "Programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OfferedCourseID = table.Column<int>(type: "int", nullable: false),
                    AttendanceDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AttendanceCreditHours = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_Attendance_OfferedCourses_OfferedCourseID",
                        column: x => x.OfferedCourseID,
                        principalTable: "OfferedCourses",
                        principalColumn: "OfferedCourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferedCourseTimeSlots",
                columns: table => new
                {
                    OfferedCourseTimeSlotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OfferedCourseID = table.Column<int>(type: "int", nullable: false),
                    TimeSlotID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferedCourseTimeSlots", x => x.OfferedCourseTimeSlotID);
                    table.ForeignKey(
                        name: "FK_OfferedCourseTimeSlots_OfferedCourses_OfferedCourseID",
                        column: x => x.OfferedCourseID,
                        principalTable: "OfferedCourses",
                        principalColumn: "OfferedCourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferedCourseTimeSlots_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OfferedCourseTimeSlots_TimeSlots_TimeSlotID",
                        column: x => x.TimeSlotID,
                        principalTable: "TimeSlots",
                        principalColumn: "TimeSlotID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredCourses",
                columns: table => new
                {
                    RegisteredCourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    OfferedCourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredCourses", x => x.RegisteredCourseID);
                    table.ForeignKey(
                        name: "FK_RegisteredCourses_OfferedCourses_OfferedCourseID",
                        column: x => x.OfferedCourseID,
                        principalTable: "OfferedCourses",
                        principalColumn: "OfferedCourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegisteredCourses_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StudentAttendance",
                columns: table => new
                {
                    StudentAttendanceID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttendanceID = table.Column<int>(type: "int", nullable: false),
                    RegisteredCourseID = table.Column<int>(type: "int", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttendance", x => x.StudentAttendanceID);
                    table.ForeignKey(
                        name: "FK_StudentAttendance_Attendance_AttendanceID",
                        column: x => x.AttendanceID,
                        principalTable: "Attendance",
                        principalColumn: "AttendanceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAttendance_RegisteredCourses_RegisteredCourseID",
                        column: x => x.RegisteredCourseID,
                        principalTable: "RegisteredCourses",
                        principalColumn: "RegisteredCourseID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_OfferedCourseID",
                table: "Attendance",
                column: "OfferedCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_InstituteID",
                table: "Buildings",
                column: "InstituteID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InstituteID",
                table: "Departments",
                column: "InstituteID");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMemberAvailabilities_FacultyMemberID",
                table: "FacultyMemberAvailabilities",
                column: "FacultyMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMemberAvailabilities_TimeID",
                table: "FacultyMemberAvailabilities",
                column: "TimeID");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMembers_DepartmentID",
                table: "FacultyMembers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedCourses_DepartmentID",
                table: "OfferedCourses",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedCourses_FacultyMemberID",
                table: "OfferedCourses",
                column: "FacultyMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedCourses_ProgramID",
                table: "OfferedCourses",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedCourses_SemesterID",
                table: "OfferedCourses",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedCourseTimeSlots_OfferedCourseID",
                table: "OfferedCourseTimeSlots",
                column: "OfferedCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedCourseTimeSlots_RoomID",
                table: "OfferedCourseTimeSlots",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedCourseTimeSlots_TimeSlotID",
                table: "OfferedCourseTimeSlots",
                column: "TimeSlotID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramRegularTimings_ProgramID",
                table: "ProgramRegularTimings",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramRegularTimings_TimeID",
                table: "ProgramRegularTimings",
                column: "TimeID");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_DepartmentID",
                table: "Programs",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSpecialTimings_ProgramID",
                table: "ProgramSpecialTimings",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSpecialTimings_TimeID",
                table: "ProgramSpecialTimings",
                column: "TimeID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredCourses_OfferedCourseID",
                table: "RegisteredCourses",
                column: "OfferedCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredCourses_StudentID",
                table: "RegisteredCourses",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAvailibilities_DepartmentID",
                table: "RoomAvailibilities",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAvailibilities_RoomID",
                table: "RoomAvailibilities",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAvailibilities_TimeID",
                table: "RoomAvailibilities",
                column: "TimeID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingID",
                table: "Rooms",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendance_AttendanceID",
                table: "StudentAttendance",
                column: "AttendanceID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendance_RegisteredCourseID",
                table: "StudentAttendance",
                column: "RegisteredCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgarmID",
                table: "Students",
                column: "ProgarmID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_TimeID",
                table: "TimeSlots",
                column: "TimeID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_TimeTableID",
                table: "TimeSlots",
                column: "TimeTableID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_InstituteID",
                table: "TimeTables",
                column: "InstituteID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_SemesterID",
                table: "TimeTables",
                column: "SemesterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FacultyMemberAvailabilities");

            migrationBuilder.DropTable(
                name: "OfferedCourseTimeSlots");

            migrationBuilder.DropTable(
                name: "ProgramRegularTimings");

            migrationBuilder.DropTable(
                name: "ProgramSpecialTimings");

            migrationBuilder.DropTable(
                name: "RoomAvailibilities");

            migrationBuilder.DropTable(
                name: "StudentAttendance");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "RegisteredCourses");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "TimeTables");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "OfferedCourses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "FacultyMembers");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Institutes");
        }
    }
}
