using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lecreventAPI.Migrations
{
    /// <inheritdoc />
    public partial class MindenUj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_events_event_details_EventId",
                table: "user_events");

            migrationBuilder.DropForeignKey(
                name: "FK_user_events_user_profiles_UserId",
                table: "user_events");

            migrationBuilder.DropIndex(
                name: "IX_user_events_EventId",
                table: "user_events");

            migrationBuilder.DropIndex(
                name: "IX_user_events_UserId",
                table: "user_events");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_user_events_EventId",
                table: "user_events",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_user_events_UserId",
                table: "user_events",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_events_event_details_EventId",
                table: "user_events",
                column: "EventId",
                principalTable: "event_details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_events_user_profiles_UserId",
                table: "user_events",
                column: "UserId",
                principalTable: "user_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
