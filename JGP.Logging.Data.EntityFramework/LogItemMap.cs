// ***********************************************************************
// Assembly         : JGP.Logging.Data.EntityFramework
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="LogItemMap.cs" company="JGP.Logging.Data.EntityFramework">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Data.EntityFramework
{
    using Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    ///     Class LogItemMap.
    ///     Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{JGP.Logging.Core.LogItem}" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{JGP.Logging.Core.LogItem}" />
    internal class LogItemMap : IEntityTypeConfiguration<LogItem>
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<LogItem> builder)
        {
            // Primary Key.
            builder.HasKey(item => item.LogId);

            // Properties.
            builder.Property(item => item.LogLevel)
                .IsRequired();

            builder.Property(item => item.OccurredOn)
                .IsRequired();

            builder.Property(item => item.Project)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(item => item.ErrorType)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(item => item.Source)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(item => item.Message)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(item => item.StackTrace)
                .IsRequired();

            // Default values.
            builder.Property(item => item.OccurredOn)
                .HasDefaultValue(DateTimeOffset.UtcNow);

            // Indexes.
            builder.HasIndex(item => item.Project)
                .HasDatabaseName("IX_LogItem_Project");

            builder.HasIndex(item => item.ErrorType)
                .HasDatabaseName("IX_LogItem_ErrorType");

            // Table & Column Mappings.
            builder.ToTable("LogItems", "dbo");
            builder.Property(item => item.LogId).HasColumnName("LogId");
            builder.Property(item => item.LogLevel).HasColumnName("LogLevel");
            builder.Property(item => item.OccurredOn).HasColumnName("OccurredOn");
            builder.Property(item => item.Project).HasColumnName("Project");
            builder.Property(item => item.ErrorType).HasColumnName("ErrorType");
            builder.Property(item => item.Source).HasColumnName("Source");
            builder.Property(item => item.Message).HasColumnName("Message");
            builder.Property(item => item.StackTrace).HasColumnName("StackTrace");
        }
    }
}