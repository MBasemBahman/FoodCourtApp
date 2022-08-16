using Microsoft.EntityFrameworkCore.Migrations;


namespace DAL.Migrations
{
    public partial class jhhvcczs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Branchs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            _ = migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "AppAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            _ = migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$vrklCYxZMkD2kRx7ityd.emnjArkz26IAkqQjb4qs3seqzBoisufe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(
                name: "Order",
                table: "Branchs");

            _ = migrationBuilder.DropColumn(
                name: "Order",
                table: "AppAttachments");

            _ = migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$2g.WlH.xJU8J3HP8leCJ.em9Nq5l3CFBRYDVnwtJkBd5YavlamNze");
        }
    }
}
