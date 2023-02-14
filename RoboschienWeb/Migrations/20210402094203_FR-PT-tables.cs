using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoboschienWeb.Migrations
{
    public partial class FRPTtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationVersionDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppName = table.Column<string>(nullable: true),
                    VersionCode = table.Column<int>(nullable: false),
                    VersionName = table.Column<string>(nullable: true),
                    MobileOsName = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationVersionDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmailConfiguration",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailType = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    EmailSubject = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    EmailFrom = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    EmailTo = table.Column<string>(unicode: false, maxLength: 300, nullable: false),
                    MailContentPart1 = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    MailContentPart2 = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    MailContentPart3 = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfiguration", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    BlobId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 100, nullable: false),
                    StartDate = table.Column<string>(maxLength: 200, nullable: true),
                    EndDate = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EmailId = table.Column<string>(maxLength: 300, nullable: true),
                    IsAccident = table.Column<string>(nullable: true),
                    IsFirstTimeIllness = table.Column<string>(nullable: true),
                    Locale = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    ReferenceNumber = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    RequestSource = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    DataPrivacyTimeStamp = table.Column<string>(nullable: true),
                    SessionKey = table.Column<string>(nullable: true),
                    SecretKey = table.Column<string>(nullable: true),
                    IsSynced = table.Column<bool>(nullable: true),
                    CertificateData = table.Column<string>(nullable: true),
                    Consent = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveInformations_FR",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    BlobId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    StartDate = table.Column<string>(nullable: true),
                    EndDate = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    EmailId = table.Column<string>(nullable: true),
                    IsAccident = table.Column<string>(nullable: true),
                    IsFirstTimeIllness = table.Column<string>(nullable: true),
                    Locale = table.Column<string>(nullable: true),
                    ReferenceNumber = table.Column<string>(nullable: true),
                    RequestSource = table.Column<string>(nullable: true),
                    DataPrivacyTimeStamp = table.Column<string>(nullable: true),
                    SessionKey = table.Column<string>(nullable: true),
                    SecretKey = table.Column<string>(nullable: true),
                    IsSynced = table.Column<bool>(nullable: true),
                    CertificateData = table.Column<string>(nullable: true),
                    Consent = table.Column<bool>(nullable: true),
                    SelectedCountryCode = table.Column<int>(nullable: false),
                    IsOcrCheck = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveInformations_FR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveInformations_PT",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    BlobId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    StartDate = table.Column<string>(nullable: true),
                    EndDate = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    EmailId = table.Column<string>(nullable: true),
                    IsAccident = table.Column<string>(nullable: true),
                    IsFirstTimeIllness = table.Column<string>(nullable: true),
                    Locale = table.Column<string>(nullable: true),
                    ReferenceNumber = table.Column<string>(nullable: true),
                    RequestSource = table.Column<string>(nullable: true),
                    DataPrivacyTimeStamp = table.Column<string>(nullable: true),
                    SessionKey = table.Column<string>(nullable: true),
                    SecretKey = table.Column<string>(nullable: true),
                    IsSynced = table.Column<bool>(nullable: true),
                    CertificateData = table.Column<string>(nullable: true),
                    Consent = table.Column<bool>(nullable: true),
                    SelectedCountryCode = table.Column<int>(nullable: false),
                    IsOcrCheck = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveInformations_PT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControllerName = table.Column<string>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    ExceptionDetails = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IllnessTypeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IllnessType = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IllnessTypeDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    JobFrequency = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    JobDescription = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobStatus1 = table.Column<string>(unicode: false, maxLength: 40, nullable: true),
                    StatusDescription = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceNumberDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReferenceNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceNumberDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LeaveId = table.Column<int>(nullable: false),
                    EmailContent = table.Column<string>(nullable: true),
                    BccMailId = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    IsEmailSent = table.Column<bool>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EmployeeLeaveInformation_FRId = table.Column<int>(nullable: true),
                    EmployeeLeaveInformation_PTId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailInformation_EmployeeLeaveInformations_FR_EmployeeLeaveInformation_FRId",
                        column: x => x.EmployeeLeaveInformation_FRId,
                        principalTable: "EmployeeLeaveInformations_FR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailInformation_EmployeeLeaveInformations_PT_EmployeeLeaveInformation_PTId",
                        column: x => x.EmployeeLeaveInformation_PTId,
                        principalTable: "EmployeeLeaveInformations_PT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailInformation_EmployeeLeaveInformation",
                        column: x => x.LeaveId,
                        principalTable: "EmployeeLeaveInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobExecutionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    StatusId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobExecutionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobExecutionDetails_JobConfiguration",
                        column: x => x.JobId,
                        principalTable: "JobConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobExecutionDetails_JobStatus",
                        column: x => x.StatusId,
                        principalTable: "JobConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__JobExecut__Statu__108B795B",
                        column: x => x.StatusId,
                        principalTable: "JobStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FK_EmailConfigurations",
                table: "EmailConfiguration",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_EmailInformation_EmployeeLeaveInformation_FRId",
                table: "EmailInformation",
                column: "EmployeeLeaveInformation_FRId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailInformation_EmployeeLeaveInformation_PTId",
                table: "EmailInformation",
                column: "EmployeeLeaveInformation_PTId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_EmailInformation_EmployeeLeaveInformation",
                table: "EmailInformation",
                column: "LeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_JobExecutionDetails_JobConfiguration",
                table: "JobExecutionDetails",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_JobExecutionDetails_JobStatus",
                table: "JobExecutionDetails",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationVersionDetails");

            migrationBuilder.DropTable(
                name: "EmailConfiguration");

            migrationBuilder.DropTable(
                name: "EmailInformation");

            migrationBuilder.DropTable(
                name: "ErrorLog");

            migrationBuilder.DropTable(
                name: "IllnessTypeDetails");

            migrationBuilder.DropTable(
                name: "JobExecutionDetails");

            migrationBuilder.DropTable(
                name: "ReferenceNumberDetails");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveInformations_FR");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveInformations_PT");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveInformation");

            migrationBuilder.DropTable(
                name: "JobConfiguration");

            migrationBuilder.DropTable(
                name: "JobStatus");
        }
    }
}
