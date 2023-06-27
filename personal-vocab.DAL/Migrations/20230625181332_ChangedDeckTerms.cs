using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace personal_vocab.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDeckTerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_DeckAndTerms_DeckAndTermId",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Groups_GroupId",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Terms_DeckAndTerms_DeckAndTermId",
                table: "Terms");

            migrationBuilder.DropTable(
                name: "DeckAndTerms");

            migrationBuilder.DropIndex(
                name: "IX_Decks_GroupId",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Decks");

            migrationBuilder.RenameColumn(
                name: "DeckAndTermId",
                table: "Terms",
                newName: "DeckTermId");

            migrationBuilder.RenameIndex(
                name: "IX_Terms_DeckAndTermId",
                table: "Terms",
                newName: "IX_Terms_DeckTermId");

            migrationBuilder.AlterColumn<long>(
                name: "GroupId",
                table: "Decks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "GroupId1",
                table: "Decks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeckTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeckId = table.Column<int>(type: "integer", nullable: false),
                    TermId = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckTerms", x => x.Id);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Terms_DeckTerms_DeckTermId",
                table: "Terms",
                column: "DeckTermId",
                principalTable: "DeckTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_DeckTerms_DeckAndTermId",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Groups_GroupId1",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Terms_DeckTerms_DeckTermId",
                table: "Terms");

            migrationBuilder.DropTable(
                name: "DeckTerms");

            migrationBuilder.DropIndex(
                name: "IX_Decks_GroupId1",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "GroupId1",
                table: "Decks");

            migrationBuilder.RenameColumn(
                name: "DeckTermId",
                table: "Terms",
                newName: "DeckAndTermId");

            migrationBuilder.RenameIndex(
                name: "IX_Terms_DeckTermId",
                table: "Terms",
                newName: "IX_Terms_DeckAndTermId");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Decks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Decks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "DeckAndTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeckId = table.Column<short>(type: "smallint", nullable: false),
                    TermId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckAndTerms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Decks_GroupId",
                table: "Decks",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_DeckAndTerms_DeckAndTermId",
                table: "Decks",
                column: "DeckAndTermId",
                principalTable: "DeckAndTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Groups_GroupId",
                table: "Decks",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Terms_DeckAndTerms_DeckAndTermId",
                table: "Terms",
                column: "DeckAndTermId",
                principalTable: "DeckAndTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
