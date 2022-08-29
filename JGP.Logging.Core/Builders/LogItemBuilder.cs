// ***********************************************************************
// Assembly         : JGP.Logging.Core
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="LogItemBuilder.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Core.Builders
{
    using Commands;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Class LogItemBuilder.
    /// </summary>
    public class LogItemBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LogItemBuilder" /> class.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        public LogItemBuilder(string projectName)
        {
            ProjectName = projectName;
        }

        /// <summary>
        ///     Gets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        private string ProjectName { get; }

        /// <summary>
        ///     Builds the log item.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>LogItem.</returns>
        public LogItem BuildLogItem(Exception exception, LogLevel logLevel)
        {
            var command = new LogItemCommand
            {
                ErrorType = exception.GetType().FullName ?? exception.GetType().Name,
                LogLevel = logLevel,
                Message = exception.Message,
                OccurredOn = DateTimeOffset.UtcNow,
                Project = ProjectName,
                Source = exception.Source ?? "Unknown",
                StackTrace = exception.ToString()
            };

            var logItem = new LogItem(command);
            logItem.AdditionalInfo = BuildAdditionalInfo(logItem.LogId);

            return logItem;
        }

        /// <summary>
        ///     Builds the additional information.
        /// </summary>
        /// <param name="logId">The log identifier.</param>
        /// <returns>AdditionalInfo.</returns>
        private static AdditionalInfo BuildAdditionalInfo(Guid logId)
        {
            var command = new AdditionalInfoCommand
            {
                Architecture = Environment.Is64BitProcess ? "x64" : "x32",
                IpAddress = IpDetector.GetIpAddress(),
                LogId = logId,
                MachineName = Environment.MachineName,
                OperatingSystem = Environment.OSVersion.Platform.ToString(),
                OperatingSystemVersion = Environment.OSVersion.Version.ToString()
            };
            return new AdditionalInfo(command);
        }
    }
}