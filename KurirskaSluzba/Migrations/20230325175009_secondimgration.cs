using Microsoft.EntityFrameworkCore.Migrations;

namespace KurirskaSluzba.Migrations
{
    public partial class secondimgration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kuriri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    GodinaRodjenja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kuriri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paketi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Posiljalac = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Primalac = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Tezina = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenaPostarine = table.Column<int>(type: "int", nullable: false),
                    KurirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paketi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paketi_Kuriri_KurirId",
                        column: x => x.KurirId,
                        principalTable: "Kuriri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kuriri",
                columns: new[] { "Id", "GodinaRodjenja", "Ime" },
                values: new object[] { 1, 1987, "Marko Petrov" });

            migrationBuilder.InsertData(
                table: "Kuriri",
                columns: new[] { "Id", "GodinaRodjenja", "Ime" },
                values: new object[] { 2, 1990, "Andrea Marin" });

            migrationBuilder.InsertData(
                table: "Kuriri",
                columns: new[] { "Id", "GodinaRodjenja", "Ime" },
                values: new object[] { 3, 1987, "Viktor Pavlov" });

            migrationBuilder.InsertData(
                table: "Paketi",
                columns: new[] { "Id", "CenaPostarine", "KurirId", "Posiljalac", "Primalac", "Tezina" },
                values: new object[,]
                {
                    { 2, 340, 1, "Galerija doo", "Salon Centar", 0.9m },
                    { 5, 800, 1, "Galanterija szr", "Prav kroj szr", 2.2m },
                    { 3, 2200, 2, "Best terarijumi", "Ljubimci sur", 5.4m },
                    { 1, 520, 3, "Slike doo", "Galerija doo", 1.1m },
                    { 4, 4500, 3, "Kul klime", "Izgradnja doo", 7.8m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paketi_KurirId",
                table: "Paketi",
                column: "KurirId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paketi");

            migrationBuilder.DropTable(
                name: "Kuriri");
        }
    }
}
