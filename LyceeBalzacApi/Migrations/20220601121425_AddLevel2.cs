using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LyceeBalzacApi.Migrations
{
    /// <inheritdoc />
    public partial class AddLevel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Level2",
                columns: table => new
                {
                    Level2Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Level2_Name_A = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level2_Name_E = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entry_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level2", x => x.Level2Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Level2");
        }
    }
}
