using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning.Migrations
{
    /// <inheritdoc />
    public partial class ReportScopeAddedInEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Scope",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scope",
                table: "Employees");
        }
    }
}
