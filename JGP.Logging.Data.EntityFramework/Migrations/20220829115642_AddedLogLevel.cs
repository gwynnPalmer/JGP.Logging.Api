// ***********************************************************************
// Assembly         :
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="20220829115642_AddedLogLevel.cs" company="">
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
    /// Class AddedLogLevel.
    /// Implements the <see cref="Migration" />
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class AddedLogLevel : Migration
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
                defaultValue: new DateTimeOffset(new DateTime(2022, 8, 29, 11, 56, 42, 678, DateTimeKind.Unspecified).AddTicks(5910), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2022, 8, 28, 20, 48, 38, 451, DateTimeKind.Unspecified).AddTicks(134), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "LogLevel",
                schema: "dbo",
                table: "LogItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <summary>
        /// Downs the specified migration builder.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogLevel",
                schema: "dbo",
                table: "LogItems");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "OccurredOn",
                schema: "dbo",
                table: "LogItems",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2022, 8, 28, 20, 48, 38, 451, DateTimeKind.Unspecified).AddTicks(134), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2022, 8, 29, 11, 56, 42, 678, DateTimeKind.Unspecified).AddTicks(5910), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
