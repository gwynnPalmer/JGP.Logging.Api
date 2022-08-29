// ***********************************************************************
// Assembly         : JGP.Logging.Services
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="NativeLoggerOptions.cs" company="JGP.Logging.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.NativeLogging;

/// <summary>
///     Class NativeLoggerOptions.
/// </summary>
public class NativeLoggerOptions
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="NativeLoggerOptions" /> class.
    /// </summary>
    public NativeLoggerOptions()
    {
    }

    /// <summary>
    ///     Gets or sets the connection string.
    /// </summary>
    /// <value>The connection string.</value>
    public string ConnectionString { get; set; }

    /// <summary>
    ///     Gets or sets the name of the project.
    /// </summary>
    /// <value>The name of the project.</value>
    public string ProjectName { get; set; }
}