using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LyceeBalzacApi.Migrations
{
    /// <inheritdoc />
    public partial class renameLevel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Level2",
                table: "Level2");

            migrationBuilder.RenameColumn(
                name: "Level2Id",
                table: "Level2",
                newName: "Level1Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Level2",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Level1Id",
                table: "Level2",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Level2",
                table: "Level2",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Level2",
                table: "Level2");

            migrationBuilder.RenameColumn(
                name: "Level1Id",
                table: "Level2",
                newName: "Level2Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Level2",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Level2Id",
                table: "Level2",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Level2",
                table: "Level2",
                column: "Level2Id");
        }
    }
}
