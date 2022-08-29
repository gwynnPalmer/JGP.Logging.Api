// ***********************************************************************
// Assembly         : JGP.Logging.Api
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="LoggingConfiguration.cs" company="JGP.Logging.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Api.Application.Configuration
{
    using NativeLogging;

    /// <summary>
    ///     Class LoggingConfiguration.
    /// </summary>
    internal static class LoggingConfiguration
    {
        /// <summary>
        ///     Configures the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
                loggingBuilder.AddNativeLogger(options =>
                {
                    configuration.GetSection("Logging").GetSection("Native").GetSection("Options").Bind(options);
                });
            });
        }
    }
}