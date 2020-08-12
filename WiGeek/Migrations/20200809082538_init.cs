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
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    InputCode = table.Column<string>(nullable: true)
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
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DiagnosisType = table.Column<string>(nullable: true)
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
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marriages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
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
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
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
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    MedicalRecordsId = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Breathe = table.Column<string>(nullable: true),
                    HeartRate = table.Column<string>(nullable: true),
                    Temperature = table.Column<string>(nullable: true)
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
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    InputCode = table.Column<string>(nullable: true)
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
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    WorkId = table.Column<int>(nullable: true),
                    MarriageId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    AdmissionTime = table.Column<DateTime>(nullable: true),
                    HospitalNo = table.Column<string>(nullable: true),
                    PatientName = table.Column<string>(nullable: true),
                    InputCode = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    PatientIdCardNo = table.Column<string>(nullable: true),
                    DiagnosisName = table.Column<string>(nullable: true),
                    DiagnosisId = table.Column<int>(nullable: true),
                    DischargedTime = table.Column<DateTime>(nullable: true),
                    WardId = table.Column<int>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    Add = table.Column<string>(nullable: true),
                    PhysicalSignsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Marriages_MarriageId",
                        column: x => x.MarriageId,
                        principalTable: "Marriages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_PhysicalSigns_PhysicalSignsId",
                        column: x => x.PhysicalSignsId,
                        principalTable: "PhysicalSigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    HospitalCode = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    MedicalRecordsId = table.Column<string>(nullable: true),
                    GroupId = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    OrderTypeId = table.Column<int>(nullable: true),
                    OrderStatusId = table.Column<int>(nullable: true),
                    OpenTime = table.Column<DateTime>(nullable: true),
                    NurseReviewTime = table.Column<DateTime>(nullable: true),
                    NurseExecuteTime = table.Column<DateTime>(nullable: true),
                    Dosage = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    WardId = table.Column<int>(nullable: true),
                    MedicalRecordsId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_MedicalRecords_MedicalRecordsId1",
                        column: x => x.MedicalRecordsId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderTypes_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DepartmentId",
                table: "MedicalRecords",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DiagnosisId",
                table: "MedicalRecords",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_MarriageId",
                table: "MedicalRecords",
                column: "MarriageId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PhysicalSignsId",
                table: "MedicalRecords",
                column: "PhysicalSignsId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_WardId",
                table: "MedicalRecords",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_WorkId",
                table: "MedicalRecords",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MedicalRecordsId1",
                table: "Orders",
                column: "MedicalRecordsId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WardId",
                table: "Orders",
                column: "WardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "Marriages");

            migrationBuilder.DropTable(
                name: "PhysicalSigns");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Works");
        }
    }
}
