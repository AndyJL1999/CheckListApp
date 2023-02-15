using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckListApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class MyTaskModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotStarted",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotStarted",
                table: "Tasks");
        }
    }
}
