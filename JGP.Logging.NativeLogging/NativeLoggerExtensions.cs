// ***********************************************************************
// Assembly         : JGP.Logging.Services
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="NativeLoggerExtensions.cs" company="JGP.Logging.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.NativeLogging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

/// <summary>
///     Class NativeLoggerExtensions.
/// </summary>
public static class NativeLoggerExtensions
{
    /// <summary>
    ///     Adds the native logger.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="configure">The configure.</param>
    /// <returns>ILoggingBuilder.</returns>
    public static ILoggingBuilder AddNativeLogger(this ILoggingBuilder builder, Action<NativeLoggerOptions> configure)
    {
        builder.Services.AddSingleton<ILoggerProvider, NativeLoggerProvider>();
        builder.Services.Configure(configure);
        return builder;
    }
}