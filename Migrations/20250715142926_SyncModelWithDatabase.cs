using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anarcareweb.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelWithDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.RenameColumn(
            //     name: "ResumeFileName",
            //     table: "Employments",
            //     newName: "ResumeUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResumeUrl",
                table: "Employments",
                newName: "ResumeFileName");
        }
    }
}
