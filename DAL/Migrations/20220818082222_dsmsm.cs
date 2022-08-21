using Microsoft.EntityFrameworkCore.Migrations;


namespace DAL.Migrations
{
    public partial class dsmsm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.AddColumn<string>(
                name: "SearchTxt",
                table: "Shops",
                type: "nvarchar(max)",
                nullable: true);

            _ = migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$D.FEWZooWbLkYHcYMFl/qu8NNkm/xBMvEV8YZeVECmTEjvnB3pvdu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(
                name: "SearchTxt",
                table: "Shops");

            _ = migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$vrklCYxZMkD2kRx7ityd.emnjArkz26IAkqQjb4qs3seqzBoisufe");
        }
    }
}
