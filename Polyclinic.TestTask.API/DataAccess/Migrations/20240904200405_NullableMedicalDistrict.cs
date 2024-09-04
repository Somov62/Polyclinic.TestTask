using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Polyclinic.TestTask.API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NullableMedicalDistrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Districts_MedicalDistrictId",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalDistrictId",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Districts_MedicalDistrictId",
                table: "Doctors",
                column: "MedicalDistrictId",
                principalTable: "Districts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Districts_MedicalDistrictId",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalDistrictId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Districts_MedicalDistrictId",
                table: "Doctors",
                column: "MedicalDistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
