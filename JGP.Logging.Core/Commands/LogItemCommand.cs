// ***********************************************************************
// Assembly         : JGP.Logging.Core
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="LogItemCommand.cs" company="JGP.Logging.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace JGP.Logging.Core.Commands
{
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Class LogItemCommand.
    /// </summary>
    public class LogItemCommand
    {
        /// <summary>
        ///     Gets or sets the type of the error.
        /// </summary>
        /// <value>The type of the error.</value>
        public string ErrorType { get; set; }

        /// <summary>
        ///     Gets or sets the log level.
        /// </summary>
        /// <value>The log level.</value>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the occurred on.
        /// </summary>
        /// <value>The occurred on.</value>
        public DateTimeOffset OccurredOn { get; set; }

        /// <summary>
        ///     Gets or sets the project.
        /// </summary>
        /// <value>The project.</value>
        public string Project { get; set; }
        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; set; }
        /// <summary>
        ///     Gets or sets the stack trace.
        /// </summary>
        /// <value>The stack trace.</value>
        public string StackTrace { get; set; }

        /// <summary>
        ///     Gets or sets the additional information.
        /// </summary>
        /// <value>The additional information.</value>
        public AdditionalInfoCommand? AdditionalInfo { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.