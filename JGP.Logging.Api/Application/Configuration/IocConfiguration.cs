// ***********************************************************************
// Assembly         : JGP.Logging.Api
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="IocConfiguration.cs" company="JGP.Logging.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using JGP.Api.KeyManagement.Authentication.Extensions;
using JGP.Logging.Data.EntityFramework;
using JGP.Logging.Services;
using Microsoft.EntityFrameworkCore;

namespace JGP.Logging.Api.Application.Configuration;

/// <summary>
///     Class IocConfiguration.
/// </summary>
internal static class IocConfiguration
{
    /// <summary>
    ///     Configures the specified services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        // Configuration.
        services.AddSingleton(configuration);

        // Logging.
        LoggingConfiguration.Configure(services, configuration);

        // Context.
        var connectionString = configuration.GetConnectionString("LogContext");
        services.AddDbContext<LogContext>(options =>
            options.UseSqlServer(connectionString, optionsBuilder =>
                optionsBuilder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null)));

        services.AddMemoryCache();

        // Custom Api Key Middleware
        services.AddApiKeyManagement(configuration);

        // Services.
        services.AddTransient<ILogContext, LogContext>();
        services.AddTransient<ILoggingService, LoggingService>();
    }
}