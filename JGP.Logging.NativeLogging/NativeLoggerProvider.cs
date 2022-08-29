// ***********************************************************************
// Assembly         : JGP.Logging.Services
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="NativeLoggerProvider.cs" company="JGP.Logging.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.NativeLogging;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <summary>
///     Class NativeLoggerProvider.
///     Implements the <see cref="ILoggerProvider" />
/// </summary>
/// <seealso cref="ILoggerProvider" />
public class NativeLoggerProvider : ILoggerProvider
{
    /// <summary>
    ///     The options
    /// </summary>
    public readonly NativeLoggerOptions Options;

    /// <summary>
    ///     Initializes a new instance of the <see cref="NativeLoggerProvider" /> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public NativeLoggerProvider(IOptions<NativeLoggerOptions> options)
    {
        Options = options.Value;
    }

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        //throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.
    /// </summary>
    /// <param name="categoryName">The category name for messages produced by the logger.</param>
    /// <returns>The instance of <see cref="T:Microsoft.Extensions.Logging.ILogger" /> that was created.</returns>
    public ILogger CreateLogger(string categoryName)
    {
        return new NativeLogger(this);
    }
}