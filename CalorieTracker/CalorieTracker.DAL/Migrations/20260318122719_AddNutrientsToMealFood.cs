using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalorieTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNutrientsToMealFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TotalCarbs",
                table: "MealFoods",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalFat",
                table: "MealFoods",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalProtein",
                table: "MealFoods",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCarbs",
                table: "MealFoods");

            migrationBuilder.DropColumn(
                name: "TotalFat",
                table: "MealFoods");

            migrationBuilder.DropColumn(
                name: "TotalProtein",
                table: "MealFoods");
        }
    }
}
