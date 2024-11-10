using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangVet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "highlightedTerms",
                columns: table => new
                {
                    HighlightedTermId = table.Column<Guid>(type: "uuid", nullable: false),
                    termName = table.Column<string>(type: "text", nullable: false),
                    termDefinition = table.Column<string>(type: "text", nullable: false),
                    termDescription = table.Column<string>(type: "text", nullable: false),
                    termLink = table.Column<string>(type: "text", nullable: false),
                    termSubCluster = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_highlightedTerms", x => x.HighlightedTermId);
                });

            migrationBuilder.CreateTable(
                name: "inputs",
                columns: table => new
                {
                    InputId = table.Column<Guid>(type: "uuid", nullable: false),
                    InputFile = table.Column<byte[]>(type: "bytea", nullable: false),
                    InputText = table.Column<string>(type: "text", nullable: false),
                    InputName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inputs", x => x.InputId);
                });

            migrationBuilder.CreateTable(
                name: "markedDocuments",
                columns: table => new
                {
                    MarkedDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    InputDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DictionaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileDownloadLink = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_markedDocuments", x => x.MarkedDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "outputTerms",
                columns: table => new
                {
                    OutputTermsId = table.Column<Guid>(type: "uuid", nullable: false),
                    DictionaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    HighlightedTermIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outputTerms", x => x.OutputTermsId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "highlightedTerms");

            migrationBuilder.DropTable(
                name: "inputs");

            migrationBuilder.DropTable(
                name: "markedDocuments");

            migrationBuilder.DropTable(
                name: "outputTerms");
        }
    }
}
