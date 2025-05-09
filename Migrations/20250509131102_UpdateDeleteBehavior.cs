using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtUnion_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkLikes_Users_UserId",
                table: "ArtworkLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Users_ArtistId",
                table: "Artworks");

            migrationBuilder.DropForeignKey(
                name: "FK_Critiques_Users_CriticId",
                table: "Critiques");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_ArtistId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_SubscriberId",
                table: "Subscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkLikes_Users_UserId",
                table: "ArtworkLikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Users_ArtistId",
                table: "Artworks",
                column: "ArtistId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Critiques_Users_CriticId",
                table: "Critiques",
                column: "CriticId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_ArtistId",
                table: "Subscriptions",
                column: "ArtistId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_SubscriberId",
                table: "Subscriptions",
                column: "SubscriberId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkLikes_Users_UserId",
                table: "ArtworkLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Users_ArtistId",
                table: "Artworks");

            migrationBuilder.DropForeignKey(
                name: "FK_Critiques_Users_CriticId",
                table: "Critiques");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_ArtistId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_SubscriberId",
                table: "Subscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkLikes_Users_UserId",
                table: "ArtworkLikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Users_ArtistId",
                table: "Artworks",
                column: "ArtistId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Critiques_Users_CriticId",
                table: "Critiques",
                column: "CriticId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_ArtistId",
                table: "Subscriptions",
                column: "ArtistId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_SubscriberId",
                table: "Subscriptions",
                column: "SubscriberId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
