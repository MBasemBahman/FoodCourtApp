using Microsoft.EntityFrameworkCore.Migrations;


namespace DAL.Migrations
{
    public partial class dmkmsms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.AddColumn<int>(
                name: "Fk_Branch",
                table: "AppAttachments",
                type: "int",
                nullable: true);

            _ = migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$p.WWjuWB6Mthg9vT95tUheEHaOHHSmGdrd75LO3VWVx.nT/779yl2");

            _ = migrationBuilder.CreateIndex(
                name: "IX_AppAttachments_Fk_Branch",
                table: "AppAttachments",
                column: "Fk_Branch");

            _ = migrationBuilder.AddForeignKey(
                name: "FK_AppAttachments_Branchs_Fk_Branch",
                table: "AppAttachments",
                column: "Fk_Branch",
                principalTable: "Branchs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropForeignKey(
                name: "FK_AppAttachments_Branchs_Fk_Branch",
                table: "AppAttachments");

            _ = migrationBuilder.DropIndex(
                name: "IX_AppAttachments_Fk_Branch",
                table: "AppAttachments");

            _ = migrationBuilder.DropColumn(
                name: "Fk_Branch",
                table: "AppAttachments");

            _ = migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$D.FEWZooWbLkYHcYMFl/qu8NNkm/xBMvEV8YZeVECmTEjvnB3pvdu");
        }
    }
}
