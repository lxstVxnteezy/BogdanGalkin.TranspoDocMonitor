using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspoDocMonitor.Service.DataContext.Migrations
{
    public partial class RefactoringDocumentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transport_documents_DictionaryDocumentTypes_dictionary_docu~",
                table: "transport_documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DictionaryDocumentTypes",
                table: "DictionaryDocumentTypes");

            migrationBuilder.RenameTable(
                name: "DictionaryDocumentTypes",
                newName: "dictionary_document_type");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "dictionary_document_type",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DocumentName",
                table: "dictionary_document_type",
                newName: "document_name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dictionary_document_type",
                table: "dictionary_document_type",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_transport_documents_dictionary_document_type_dictionary_doc~",
                table: "transport_documents",
                column: "dictionary_document_type_id",
                principalTable: "dictionary_document_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
           
            migrationBuilder.InsertData(
                table: "dictionary_document_type",
                columns: new[] { "id", "document_name" },
                values: new object[,]
                {
                    { new Guid("8f98b303-ed97-4a8f-88d0-336be0915342"), "CCLI" }, // Assuming 1 is a new ID
                    { new Guid("55b9c6e4-829b-46f1-87d6-2a92fc5060dd"), "CCI" }   // Assuming 2 is a new ID
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transport_documents_dictionary_document_type_dictionary_doc~",
                table: "transport_documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dictionary_document_type",
                table: "dictionary_document_type");

            migrationBuilder.RenameTable(
                name: "dictionary_document_type",
                newName: "DictionaryDocumentTypes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DictionaryDocumentTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "document_name",
                table: "DictionaryDocumentTypes",
                newName: "DocumentName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DictionaryDocumentTypes",
                table: "DictionaryDocumentTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_transport_documents_DictionaryDocumentTypes_dictionary_docu~",
                table: "transport_documents",
                column: "dictionary_document_type_id",
                principalTable: "DictionaryDocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
