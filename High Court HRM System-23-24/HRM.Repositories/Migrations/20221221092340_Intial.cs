using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.Repositories.Migrations
{
    public partial class Intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CNIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonnelNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTP_Verify = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentBranchId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Branches_ParentBranchId",
                        column: x => x.ParentBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DegreeLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreeLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignationTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BPSNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BPSID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domiciles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomicileTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domiciles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LHCData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LHCData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LOVs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderBy = table.Column<int>(type: "int", nullable: false),
                    LOV_Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOVs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNICFrontURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNICBackURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DomicileURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Domicile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomicileOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DomicileId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalityOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalityId = table.Column<int>(type: "int", nullable: false),
                    ReligionOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReligionId = table.Column<int>(type: "int", nullable: false),
                    MaritalStatusOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelfContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencContactRelation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HighestQualificationLevelAtAppointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighestQualificationLevelAtAppointmentId = table.Column<int>(type: "int", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonalFileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonnelNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentDesignationId = table.Column<int>(type: "int", nullable: false),
                    CurrentDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentDesignationOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentPostingBranchId = table.Column<int>(type: "int", nullable: false),
                    CurrentPostingBranch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPlacePostingBenchId = table.Column<int>(type: "int", nullable: false),
                    CurrentPlacePostingBench = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentPayScale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPayScaleId = table.Column<int>(type: "int", nullable: false),
                    CurrentPaySlipURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DegreeTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DegreeLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreeTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DegreeTitles_DegreeLevels_DegreeLevelId",
                        column: x => x.DegreeLevelId,
                        principalTable: "DegreeLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomNotification_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualitifationTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfResult = table.Column<int>(type: "int", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MarksObtained = table.Column<double>(type: "float", nullable: false),
                    TotalMarks = table.Column<double>(type: "float", nullable: false),
                    MarksType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherMarkObtain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherMarkTotal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardUniversity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree_Front_URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Degree_Back_URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeOrDivisionOrPass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualificationTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPlacedOnRecord = table.Column<bool>(type: "bit", nullable: true),
                    DegreeId = table.Column<int>(type: "int", nullable: false),
                    DegreeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DegreeNameOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeLevelId = table.Column<int>(type: "int", nullable: false),
                    DegreeLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NOC = table.Column<bool>(type: "bit", nullable: false),
                    NOCURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualifications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayScale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayScaleId = table.Column<int>(type: "int", nullable: false),
                    Deparment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinThroughProperChannel = table.Column<bool>(type: "bit", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: true),
                    To = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RelievingURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceHistories_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentAs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentAsId = table.Column<int>(type: "int", nullable: false),
                    AppointmentBPS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentBPSId = table.Column<int>(type: "int", nullable: false),
                    AppiontmentLetterURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfAppointmentLetter = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfChargeAssumption = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuotaOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuotaId = table.Column<int>(type: "int", nullable: false),
                    ModeOfAppointmentOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModeOfAppointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModeOfAppointmentId = table.Column<int>(type: "int", nullable: false),
                    JoinThroughProperChannel = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ServiceHistoryId = table.Column<int>(type: "int", nullable: true),
                    ServiceHistoryText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: true),
                    VerifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_ServiceHistories_ServiceHistoryId",
                        column: x => x.ServiceHistoryId,
                        principalTable: "ServiceHistories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId",
                table: "Appointments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceHistoryId",
                table: "Appointments",
                column: "ServiceHistoryId");

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
                name: "IX_Branches_ParentBranchId",
                table: "Branches",
                column: "ParentBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomNotification_EmployeeId",
                table: "CustomNotification",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DegreeTitles_DegreeLevelId",
                table: "DegreeTitles",
                column: "DegreeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_EmployeeId",
                table: "Qualifications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceHistories_EmployeeId",
                table: "ServiceHistories",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

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
                name: "Branches");

            migrationBuilder.DropTable(
                name: "CustomNotification");

            migrationBuilder.DropTable(
                name: "DegreeTitles");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Domiciles");

            migrationBuilder.DropTable(
                name: "LHCData");

            migrationBuilder.DropTable(
                name: "LOVs");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "ServiceHistories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DegreeLevels");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
