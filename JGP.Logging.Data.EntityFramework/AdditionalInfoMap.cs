// ***********************************************************************
// Assembly         : JGP.Logging.Data.EntityFramework
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="AdditionalInfoMap.cs" company="JGP.Logging.Data.EntityFramework">
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
    ///     Class AdditionalInfoMap.
    ///     Implements the
    ///     <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{JGP.Logging.Core.AdditionalInfo}" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{JGP.Logging.Core.AdditionalInfo}" />
    internal class AdditionalInfoMap : IEntityTypeConfiguration<AdditionalInfo>
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<AdditionalInfo> builder)
        {
            // Primary Key.
            builder.HasKey(info => info.LogId);

            // Properties.
            builder.Property(item => item.LogId)
                .IsRequired();

            // Indexes
            builder.HasIndex(item => item.IpAddress)
                .HasDatabaseName("IX_AdditionalInfo_IpAddress");

            // Table & Column Mappings.
            builder.ToTable("AdditionalInfo", "dbo");
            builder.Property(item => item.LogId).HasColumnName("LogId");
            builder.Property(item => item.MachineName).HasColumnName("MachineName");
            builder.Property(item => item.IpAddress).HasColumnName("IpAddress");
            builder.Property(item => item.OperatingSystem).HasColumnName("OperatingSystem");
            builder.Property(item => item.OperatingSystemVersion).HasColumnName("OperatingSystemVersion");
            builder.Property(item => item.Architecture).HasColumnName("Architecture");

            // Relationships.
            builder.HasOne(info => info.LogItem)
                .WithOne(item => item.AdditionalInfo)
                .HasForeignKey<AdditionalInfo>(item => item.LogId)
                .HasConstraintName("FK_AdditionalInfo_LogItems_LogId");
        }
    }
}