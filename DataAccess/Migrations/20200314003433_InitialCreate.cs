using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    EMail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    DocumentType = table.Column<int>(nullable: false),
                    DocumentNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relations",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    User2Id = table.Column<int>(nullable: false),
                    Relation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => new { x.UserId, x.User2Id });
                    table.ForeignKey(
                        name: "FK_Relations_User_User2Id",
                        column: x => x.User2Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Relations_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relations_User2Id",
                table: "Relations",
                column: "User2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
