using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class dmkmsms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Fk_Branch",
                table: "AppAttachments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$p.WWjuWB6Mthg9vT95tUheEHaOHHSmGdrd75LO3VWVx.nT/779yl2");

            migrationBuilder.CreateIndex(
                name: "IX_AppAttachments_Fk_Branch",
                table: "AppAttachments",
                column: "Fk_Branch");

            migrationBuilder.AddForeignKey(
                name: "FK_AppAttachments_Branchs_Fk_Branch",
                table: "AppAttachments",
                column: "Fk_Branch",
                principalTable: "Branchs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppAttachments_Branchs_Fk_Branch",
                table: "AppAttachments");

            migrationBuilder.DropIndex(
                name: "IX_AppAttachments_Fk_Branch",
                table: "AppAttachments");

            migrationBuilder.DropColumn(
                name: "Fk_Branch",
                table: "AppAttachments");

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$D.FEWZooWbLkYHcYMFl/qu8NNkm/xBMvEV8YZeVECmTEjvnB3pvdu");
        }
    }
}
