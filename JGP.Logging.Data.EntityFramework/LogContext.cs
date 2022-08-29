// ***********************************************************************
// Assembly         : JGP.Logging.Data.EntityFramework
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="LogContext.cs" company="JGP.Logging.Data.EntityFramework">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Data.EntityFramework
{
    using Core;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Class LogContext.
    ///     Implements the <see cref="DbContext" />
    /// </summary>
    /// <seealso cref="DbContext" />
    public class LogContext : DbContext, ILogContext
    {
        /// <summary>
        ///     The local connection string
        /// </summary>
        private const string LocalConnectionString =
            @"Server=.;Database=jgp-logging;Persist Security Info=True;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;Application Name=Logging Service";

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogContext" /> class.
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
        ///     for more information.
        /// </remarks>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LogContext()
        {
            base.Database.SetCommandTimeout(1000);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        #region DBSETS

        /// <summary>
        ///     Gets or sets the log items.
        /// </summary>
        /// <value>The log items.</value>
        public DbSet<LogItem> LogItems { get; set; }

        /// <summary>
        ///     Gets or sets the additional information.
        /// </summary>
        /// <value>The additional information.</value>
        public DbSet<AdditionalInfo> AdditionalInfo { get; set; }

        #endregion


        /// <summary>
        ///     <para>
        ///         Override this method to configure the database (and other options) to be used for this context.
        ///         This method is called for each instance of the context that is created.
        ///         The base implementation does nothing.
        ///     </para>
        ///     <para>
        ///         In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may
        ///         not have been passed
        ///         to the constructor, you can use
        ///         <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        ///         the options have already been set, and skip some or all of the logic in
        ///         <see
        ///             cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />
        ///         .
        ///     </para>
        /// </summary>
        /// <param name="optionsBuilder">
        ///     A builder used to create or modify options for this context. Databases (and other extensions)
        ///     typically define extension methods on this object that allow you to configure the context.
        /// </param>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
        ///     for more information.
        /// </remarks>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(LocalConnectionString);
            }
        }

        /// <summary>
        ///     Override this method to further configure the model that was discovered by convention from the entity types
        ///     exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting
        ///     model may be cached
        ///     and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">
        ///     The builder being used to construct the model for this context. Databases (and other extensions) typically
        ///     define extension methods on this object that allow you to configure aspects of the model that are specific
        ///     to a given database.
        /// </param>
        /// <remarks>
        ///     <para>
        ///         If a model is explicitly set on the options for this context (via
        ///         <see
        ///             cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />
        ///         )
        ///         then this method will not be run.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more
        ///         information.
        ///     </para>
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogItemMap());
            modelBuilder.ApplyConfiguration(new AdditionalInfoMap());
        }
    }
}