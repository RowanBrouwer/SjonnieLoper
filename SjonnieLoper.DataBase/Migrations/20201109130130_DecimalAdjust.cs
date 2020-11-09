using Microsoft.EntityFrameworkCore.Migrations;

namespace SjonnieLoper.DataBase.Migrations
{
    public partial class DecimalAdjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Whiskeys",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage",
                table: "Whiskeys",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.UpdateData(
                table: "Whiskeys",
                keyColumn: "Id",
                keyValue: 5,
                column: "AgeYears",
                value: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Whiskeys",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage",
                table: "Whiskeys",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Whiskeys",
                keyColumn: "Id",
                keyValue: 5,
                column: "AgeYears",
                value: 4);
        }
    }
}
