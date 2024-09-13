using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnonKey_Backend.Migrations
{
    /// <inheritdoc />
    public partial class FolderMigrationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserUuid",
                table: "Folders",
                newName: "Uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uuid",
                table: "Folders",
                newName: "UserUuid");
        }
    }
}
