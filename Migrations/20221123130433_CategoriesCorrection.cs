using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lamiapizzeriastatic.Migrations
{
    /// <inheritdoc />
    public partial class CategoriesCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Pizzas_pizzaId",
                table: "Pizzas");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_pizzaId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "pizzaId",
                table: "Pizzas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pizzaId",
                table: "Pizzas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_pizzaId",
                table: "Pizzas",
                column: "pizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Pizzas_pizzaId",
                table: "Pizzas",
                column: "pizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id");
        }
    }
}
