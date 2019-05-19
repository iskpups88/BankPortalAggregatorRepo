using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankPortalAggregator.Migrations
{
    public partial class DepositVariationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percent",
                table: "Deposits");

            migrationBuilder.CreateTable(
                name: "DepositVariations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Term = table.Column<string>(nullable: true),
                    Percent = table.Column<decimal>(nullable: false),
                    DepositId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositVariations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepositVariations_Deposits_DepositId",
                        column: x => x.DepositId,
                        principalTable: "Deposits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepositVariations_DepositId",
                table: "DepositVariations",
                column: "DepositId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepositVariations");

            migrationBuilder.AddColumn<float>(
                name: "Percent",
                table: "Deposits",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
