// ***********************************************************************
// Assembly         :
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="20220829131818_AddedAdditionalInfo.cs" company="">
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
    /// Class AddedAdditionalInfo.
    /// Implements the <see cref="Migration" />
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class AddedAdditionalInfo : Migration
    {
        /// <summary>
        /// Ups the specified migration builder.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "OccurredOn",
                schema: "dbo",
                table: "LogItems",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2022, 8, 29, 13, 18, 18, 578, DateTimeKind.Unspecified).AddTicks(2555), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2022, 8, 29, 11, 56, 42, 678, DateTimeKind.Unspecified).AddTicks(5910), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "AdditionalInfo",
                schema: "dbo",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OperatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperatingSystemVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Architecture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInfo", x => x.LogId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfo_IpAddress",
                schema: "dbo",
                table: "AdditionalInfo",
                column: "IpAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalInfo_LogItems_LogId",
                schema: "dbo",
                table: "LogItems",
                column: "LogId",
                principalSchema: "dbo",
                principalTable: "AdditionalInfo",
                principalColumn: "LogId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <summary>
        /// Downs the specified migration builder.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalInfo_LogItems_LogId",
                schema: "dbo",
                table: "LogItems");

            migrationBuilder.DropTable(
                name: "AdditionalInfo",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "OccurredOn",
                schema: "dbo",
                table: "LogItems",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2022, 8, 29, 11, 56, 42, 678, DateTimeKind.Unspecified).AddTicks(5910), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2022, 8, 29, 13, 18, 18, 578, DateTimeKind.Unspecified).AddTicks(2555), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
