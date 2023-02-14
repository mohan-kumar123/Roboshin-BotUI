using Microsoft.EntityFrameworkCore.Migrations;

namespace RoboschienWeb.Migrations
{
    public partial class countrycodetypechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailInformation_EmployeeLeaveInformations_FR_EmployeeLeaveInformation_FRId",
                table: "EmailInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailInformation_EmployeeLeaveInformations_PT_EmployeeLeaveInformation_PTId",
                table: "EmailInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveInformations_PT",
                table: "EmployeeLeaveInformations_PT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveInformations_FR",
                table: "EmployeeLeaveInformations_FR");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveInformations_PT",
                newName: "EmployeeLeaveInformation_PT");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveInformations_FR",
                newName: "EmployeeLeaveInformation_FR");

            migrationBuilder.RenameColumn(
                name: "IsAccident",
                table: "EmployeeLeaveInformation_PT",
                newName: "SickLeaveType");

            migrationBuilder.RenameColumn(
                name: "IsAccident",
                table: "EmployeeLeaveInformation_FR",
                newName: "IsWorkAccident");

            migrationBuilder.AddColumn<string>(
                name: "SelectedCountryCode",
                table: "EmployeeLeaveInformation",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SelectedCountryCode",
                table: "EmployeeLeaveInformation_PT",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "SelectedCountryCode",
                table: "EmployeeLeaveInformation_FR",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "IsHospitalization",
                table: "EmployeeLeaveInformation_FR",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveInformation_PT",
                table: "EmployeeLeaveInformation_PT",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveInformation_FR",
                table: "EmployeeLeaveInformation_FR",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailInformation_EmployeeLeaveInformation_FR_EmployeeLeaveInformation_FRId",
                table: "EmailInformation",
                column: "EmployeeLeaveInformation_FRId",
                principalTable: "EmployeeLeaveInformation_FR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailInformation_EmployeeLeaveInformation_PT_EmployeeLeaveInformation_PTId",
                table: "EmailInformation",
                column: "EmployeeLeaveInformation_PTId",
                principalTable: "EmployeeLeaveInformation_PT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailInformation_EmployeeLeaveInformation_FR_EmployeeLeaveInformation_FRId",
                table: "EmailInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailInformation_EmployeeLeaveInformation_PT_EmployeeLeaveInformation_PTId",
                table: "EmailInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveInformation_PT",
                table: "EmployeeLeaveInformation_PT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveInformation_FR",
                table: "EmployeeLeaveInformation_FR");

            migrationBuilder.DropColumn(
                name: "SelectedCountryCode",
                table: "EmployeeLeaveInformation");

            migrationBuilder.DropColumn(
                name: "IsHospitalization",
                table: "EmployeeLeaveInformation_FR");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveInformation_PT",
                newName: "EmployeeLeaveInformations_PT");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveInformation_FR",
                newName: "EmployeeLeaveInformations_FR");

            migrationBuilder.RenameColumn(
                name: "SickLeaveType",
                table: "EmployeeLeaveInformations_PT",
                newName: "IsAccident");

            migrationBuilder.RenameColumn(
                name: "IsWorkAccident",
                table: "EmployeeLeaveInformations_FR",
                newName: "IsAccident");

            migrationBuilder.AlterColumn<int>(
                name: "SelectedCountryCode",
                table: "EmployeeLeaveInformations_PT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SelectedCountryCode",
                table: "EmployeeLeaveInformations_FR",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveInformations_PT",
                table: "EmployeeLeaveInformations_PT",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveInformations_FR",
                table: "EmployeeLeaveInformations_FR",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailInformation_EmployeeLeaveInformations_FR_EmployeeLeaveInformation_FRId",
                table: "EmailInformation",
                column: "EmployeeLeaveInformation_FRId",
                principalTable: "EmployeeLeaveInformations_FR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailInformation_EmployeeLeaveInformations_PT_EmployeeLeaveInformation_PTId",
                table: "EmailInformation",
                column: "EmployeeLeaveInformation_PTId",
                principalTable: "EmployeeLeaveInformations_PT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
