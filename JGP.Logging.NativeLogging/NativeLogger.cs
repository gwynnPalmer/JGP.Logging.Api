// ***********************************************************************
// Assembly         : JGP.Logging.Services
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="NativeLogger.cs" company="JGP.Logging.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.NativeLogging
{
    using Core.Builders;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Class NativeLogger.
    ///     Implements the <see cref="ILogger" />
    /// </summary>
    /// <seealso cref="ILogger" />
    public class NativeLogger : ILogger
    {
        /// <summary>
        ///     The log item builder
        /// </summary>
        private readonly LogItemBuilder _logItemBuilder;

        /// <summary>
        ///     The log writer
        /// </summary>
        private readonly LogWriter _logWriter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeLogger" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public NativeLogger(NativeLoggerProvider provider)
        {
            _logItemBuilder = new LogItemBuilder(provider.Options.ProjectName);
            _logWriter = new LogWriter(provider.Options.ConnectionString);
        }

        /// <summary>
        ///     Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">The type of the state to begin scope for.</typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>An <see cref="T:System.IDisposable" /> that ends the logical operation scope on dispose.</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        ///     Checks if the given <paramref name="logLevel" /> is enabled.
        /// </summary>
        /// <param name="logLevel">Level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        /// <summary>
        ///     Writes a log entry.
        /// </summary>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">
        ///     Function to create a <see cref="T:System.String" /> message of the <paramref name="state" />
        ///     and <paramref name="exception" />.
        /// </param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            if (exception == null) return;

            var logItem = _logItemBuilder.BuildLogItem(exception, logLevel);
            _logWriter.LogError(logItem);
        }
    }
}