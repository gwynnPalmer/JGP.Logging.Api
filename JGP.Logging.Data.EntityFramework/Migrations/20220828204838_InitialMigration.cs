// ***********************************************************************
// Assembly         :
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="20220828204838_InitialMigration.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JGP.Logging.Data.EntityFramework.Migrations
{
    /// <summary>
    /// Class InitialMigration.
    /// Implements the <see cref="Migration" />
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class InitialMigration : Migration
    {
        /// <summary>
        /// Ups the specified migration builder.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "LogItems",
                schema: "dbo",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2022, 8, 28, 20, 48, 38, 451, DateTimeKind.Unspecified).AddTicks(134), new TimeSpan(0, 0, 0, 0, 0))),
                    Project = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ErrorType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogItems", x => x.LogId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogItem_ErrorType",
                schema: "dbo",
                table: "LogItems",
                column: "ErrorType");

            migrationBuilder.CreateIndex(
                name: "IX_LogItem_Project",
                schema: "dbo",
                table: "LogItems",
                column: "Project");
        }

        /// <summary>
        /// Downs the specified migration builder.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogItems",
                schema: "dbo");
        }
    }
}
