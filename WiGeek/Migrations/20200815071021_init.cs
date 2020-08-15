using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WiGeek.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    InputCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    DiagnosisType = table.Column<string>(maxLength: 50, nullable: true),
                    IsDel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marriages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marriages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    WorkId = table.Column<string>(maxLength: 50, nullable: true),
                    MarriageId = table.Column<string>(maxLength: 50, nullable: true),
                    DepartmentId = table.Column<string>(maxLength: 50, nullable: true),
                    AdmissionTime = table.Column<DateTime>(nullable: true),
                    HospitalNo = table.Column<string>(maxLength: 50, nullable: true),
                    PatientName = table.Column<string>(maxLength: 500, nullable: true),
                    InputCode = table.Column<string>(maxLength: 50, nullable: true),
                    Sex = table.Column<string>(maxLength: 10, nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    PatientIdCardNo = table.Column<string>(maxLength: 18, nullable: true),
                    DiagnosisName = table.Column<string>(maxLength: 500, nullable: true),
                    DiagnosisId = table.Column<string>(maxLength: 50, nullable: true),
                    DischargedTime = table.Column<DateTime>(nullable: true),
                    WardId = table.Column<string>(maxLength: 50, nullable: true),
                    ContactPerson = table.Column<string>(maxLength: 500, nullable: true),
                    ContactPhone = table.Column<string>(maxLength: 100, nullable: true),
                    Add = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    MedicalRecordsId = table.Column<string>(maxLength: 50, nullable: true),
                    GroupId = table.Column<string>(maxLength: 50, nullable: true),
                    DepartmentId = table.Column<string>(maxLength: 50, nullable: true),
                    Type = table.Column<string>(maxLength: 50, nullable: true),
                    OrderTypeId = table.Column<string>(maxLength: 50, nullable: true),
                    OrderStatusId = table.Column<string>(maxLength: 50, nullable: true),
                    OpenTime = table.Column<DateTime>(nullable: true),
                    NurseReviewTime = table.Column<DateTime>(nullable: true),
                    NurseExecuteTime = table.Column<DateTime>(nullable: true),
                    Dosage = table.Column<string>(maxLength: 50, nullable: true),
                    ExecuteTime = table.Column<DateTime>(nullable: true),
                    Unit = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    IsDel = table.Column<bool>(nullable: false),
                    WardId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalSigns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    MedicalRecordsId = table.Column<string>(maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Breathe = table.Column<string>(nullable: true),
                    HeartRate = table.Column<string>(maxLength: 50, nullable: true),
                    Temperature = table.Column<string>(maxLength: 50, nullable: true),
                    IsDel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalSigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    InputCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "Marriages");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropTable(
                name: "PhysicalSigns");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Works");
        }
    }
}
