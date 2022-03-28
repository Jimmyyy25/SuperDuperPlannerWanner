using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperDuperPlannerWanner.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MealTypeId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateAmended = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Id);
                });
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meal");
        }
    }
}
