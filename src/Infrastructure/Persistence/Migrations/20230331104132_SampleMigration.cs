using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Saiketsu.Service.Election.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SampleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "candidate",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "election_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_election_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "election",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    owner_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_election", x => x.id);
                    table.ForeignKey(
                        name: "fk_election_election_type_entity_type_id",
                        column: x => x.type_id,
                        principalTable: "election_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_election_user_entity_owner_id",
                        column: x => x.owner_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "election_candidate",
                columns: table => new
                {
                    election_id = table.Column<int>(type: "integer", nullable: false),
                    candidate_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_election_candidate", x => new { x.candidate_id, x.election_id });
                    table.ForeignKey(
                        name: "fk_election_candidate_candidate_candidate_id",
                        column: x => x.candidate_id,
                        principalTable: "candidate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_election_candidate_elections_election_id",
                        column: x => x.election_id,
                        principalTable: "election",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "election_user",
                columns: table => new
                {
                    election_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    voted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_election_user", x => new { x.user_id, x.election_id });
                    table.ForeignKey(
                        name: "fk_election_user_election_election_id",
                        column: x => x.election_id,
                        principalTable: "election",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_election_user_user_entity_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "election_type",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "FirstPassTheVote" },
                    { 2, "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_election_owner_id",
                table: "election",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_election_type_id",
                table: "election",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_election_candidate_election_id",
                table: "election_candidate",
                column: "election_id");

            migrationBuilder.CreateIndex(
                name: "ix_election_user_election_id",
                table: "election_user",
                column: "election_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "election_candidate");

            migrationBuilder.DropTable(
                name: "election_user");

            migrationBuilder.DropTable(
                name: "candidate");

            migrationBuilder.DropTable(
                name: "election");

            migrationBuilder.DropTable(
                name: "election_type");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
