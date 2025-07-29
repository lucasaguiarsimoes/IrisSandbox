using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrisSandbox.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amostra",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Iris:Identity", "1, 1"),
                    Identificacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pedido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Origem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(12,7)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amostra", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguradorGeral",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Iris:Identity", "1, 1"),
                    Item = table.Column<int>(type: "int", nullable: false),
                    ValorString = table.Column<string>(type: "nvarchar(2066)", maxLength: 2066, nullable: true),
                    ValorBoolean = table.Column<bool>(type: "bit", nullable: true),
                    ValorLong = table.Column<long>(type: "bigint", nullable: true),
                    ValorDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ValorBytes = table.Column<byte[]>(type: "varbinary(3641144)", nullable: true),
                    ValorDecimal = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguradorGeral", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Exame",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Iris:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(3641144)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(3641144)", nullable: false),
                    QuantidadeTeste = table.Column<int>(type: "int", nullable: false),
                    ExibeTeste = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(3641144)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exame", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AmostraExame",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Iris:Identity", "1, 1"),
                    AmostraID = table.Column<long>(type: "bigint", nullable: false),
                    ExameID = table.Column<long>(type: "bigint", nullable: false),
                    DataHoraProducao = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmostraExame", x => x.ID);
                    table.UniqueConstraint("AK_AmostraExame_AmostraID_ExameID", x => new { x.AmostraID, x.ExameID });
                    table.ForeignKey(
                        name: "FK_AmostraExame_Amostra_AmostraID",
                        column: x => x.AmostraID,
                        principalTable: "Amostra",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmostraExame_Exame_ExameID",
                        column: x => x.ExameID,
                        principalTable: "Exame",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amostra_Identificacao",
                table: "Amostra",
                column: "Identificacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmostraExame_AmostraID",
                table: "AmostraExame",
                column: "AmostraID");

            migrationBuilder.CreateIndex(
                name: "IX_AmostraExame_DataHoraProducao",
                table: "AmostraExame",
                column: "DataHoraProducao");

            migrationBuilder.CreateIndex(
                name: "IX_AmostraExame_ExameID",
                table: "AmostraExame",
                column: "ExameID");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguradorGeral_Item",
                table: "ConfiguradorGeral",
                column: "Item",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmostraExame");

            migrationBuilder.DropTable(
                name: "ConfiguradorGeral");

            migrationBuilder.DropTable(
                name: "Amostra");

            migrationBuilder.DropTable(
                name: "Exame");
        }
    }
}
