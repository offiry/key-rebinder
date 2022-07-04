using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Persistance.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisplayUIGameName = table.Column<string>(maxLength: 450, nullable: false),
                    WindowGameName = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyModifierEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(maxLength: 450, nullable: false),
                    KeyModifier = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyModifierEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeysEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeysEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameKeysEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KeyId_FK = table.Column<int>(nullable: false),
                    KeyModifierId_FK = table.Column<int>(nullable: false),
                    WindowGameNameId_FK = table.Column<int>(nullable: false),
                    TargetKey_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameKeysEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameKeysEntities_KeysEntities_KeyId_FK",
                        column: x => x.KeyId_FK,
                        principalTable: "KeysEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameKeysEntities_KeyModifierEntities_KeyModifierId_FK",
                        column: x => x.KeyModifierId_FK,
                        principalTable: "KeyModifierEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameKeysEntities_KeysEntities_TargetKey_FK",
                        column: x => x.TargetKey_FK,
                        principalTable: "KeysEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameKeysEntities_GameEntities_WindowGameNameId_FK",
                        column: x => x.WindowGameNameId_FK,
                        principalTable: "GameEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameKeysEntities_KeyId_FK",
                table: "GameKeysEntities",
                column: "KeyId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_GameKeysEntities_KeyModifierId_FK",
                table: "GameKeysEntities",
                column: "KeyModifierId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_GameKeysEntities_TargetKey_FK",
                table: "GameKeysEntities",
                column: "TargetKey_FK");

            migrationBuilder.CreateIndex(
                name: "IX_GameKeysEntities_WindowGameNameId_FK",
                table: "GameKeysEntities",
                column: "WindowGameNameId_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameKeysEntities");

            migrationBuilder.DropTable(
                name: "KeysEntities");

            migrationBuilder.DropTable(
                name: "KeyModifierEntities");

            migrationBuilder.DropTable(
                name: "GameEntities");
        }
    }
}
