using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstadoCuenta.Data.Migrations
{
    public partial class UpdateTransaccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SaldoDisponible",
                table: "Transaccion",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaldoDisponible",
                table: "Transaccion");
        }
    }
}
