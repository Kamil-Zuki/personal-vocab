using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace sm_repetition_algorithm.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PrimaryKeyTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_DeckTerms_DeckAndTermId",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Groups_GroupId1",
                table: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_Decks_GroupId1",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "GroupId1",
                table: "Decks");

            migrationBuilder.RenameColumn(
                name: "DeckAndTermId",
                table: "Decks",
                newName: "DeckTermId");

            migrationBuilder.RenameIndex(
                name: "IX_Decks_DeckAndTermId",
                table: "Decks",
                newName: "IX_Decks_DeckTermId");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Decks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Decks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Decks_GroupId",
                table: "Decks",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_DeckTerms_DeckTermId",
                table: "Decks",
                column: "DeckTermId",
                principalTable: "DeckTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Groups_GroupId",
                table: "Decks",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_DeckTerms_DeckTermId",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Groups_GroupId",
                table: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_Decks_GroupId",
                table: "Decks");

            migrationBuilder.RenameColumn(
                name: "DeckTermId",
                table: "Decks",
                newName: "DeckAndTermId");

            migrationBuilder.RenameIndex(
                name: "IX_Decks_DeckTermId",
                table: "Decks",
                newName: "IX_Decks_DeckAndTermId");

            migrationBuilder.AlterColumn<long>(
                name: "GroupId",
                table: "Decks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<short>(
                name: "Id",
                table: "Decks",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "GroupId1",
                table: "Decks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Decks_GroupId1",
                table: "Decks",
                column: "GroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_DeckTerms_DeckAndTermId",
                table: "Decks",
                column: "DeckAndTermId",
                principalTable: "DeckTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Groups_GroupId1",
                table: "Decks",
                column: "GroupId1",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
