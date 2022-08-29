// ***********************************************************************
// Assembly         :
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="20220829132211_TweakAdditionalInfoMap.cs" company="">
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
    /// Class TweakAdditionalInfoMap.
    /// Implements the <see cref="Migration" />
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class TweakAdditionalInfoMap : Migration
    {
        /// <summary>
        /// Ups the specified migration builder.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalInfo_LogItems_LogId",
                schema: "dbo",
                table: "LogItems");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "OccurredOn",
                schema: "dbo",
                table: "LogItems",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2022, 8, 29, 13, 22, 11, 329, DateTimeKind.Unspecified).AddTicks(3272), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2022, 8, 29, 13, 18, 18, 578, DateTimeKind.Unspecified).AddTicks(2555), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalInfo_LogItems_LogId",
                schema: "dbo",
                table: "AdditionalInfo",
                column: "LogId",
                principalSchema: "dbo",
                principalTable: "LogItems",
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
                table: "AdditionalInfo");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "OccurredOn",
                schema: "dbo",
                table: "LogItems",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2022, 8, 29, 13, 18, 18, 578, DateTimeKind.Unspecified).AddTicks(2555), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2022, 8, 29, 13, 22, 11, 329, DateTimeKind.Unspecified).AddTicks(3272), new TimeSpan(0, 0, 0, 0, 0)));

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
    }
}
