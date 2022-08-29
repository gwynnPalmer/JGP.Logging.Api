// ***********************************************************************
// Assembly         : JGP.Logging.Web.Models
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="LogItemModel.cs" company="JGP.Logging.Web.Models">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Core;
    using Core.Commands;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Class LogItemModel.
    /// </summary>
    public class LogItemModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LogItemModel"/> class.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LogItemModel()
        {
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogItemModel" /> class.
        /// </summary>
        /// <param name="logItem">The log item.</param>
        public LogItemModel(LogItem logItem)
        {
            LogId = logItem.LogId;
            LogLevel = logItem.LogLevel;
            OccurredOn = logItem.OccurredOn;
            Project = logItem.Project;
            ErrorType = logItem.ErrorType;
            Source = logItem.Source;
            Message = logItem.Message;
            StackTrace = logItem.StackTrace;
        }

        /// <summary>
        ///     Gets or sets the log identifier.
        /// </summary>
        /// <value>The log identifier.</value>
        [JsonPropertyName("logId")]
        public Guid LogId { get; set; }

        /// <summary>
        ///     Gets or sets the log level.
        /// </summary>
        /// <value>The log level.</value>
        [JsonPropertyName("logLevel")]
        public LogLevel LogLevel { get; set; }

        /// <summary>
        ///     Gets or sets the occurred on.
        /// </summary>
        /// <value>The occurred on.</value>
        [Required]
        [JsonPropertyName("occurredOn")]
        public DateTimeOffset OccurredOn { get; set; }

        /// <summary>
        ///     Gets or sets the project.
        /// </summary>
        /// <value>The project.</value>
        [Required]
        [MaxLength(255)]
        [JsonPropertyName("project")]
        public string Project { get; set; }

        /// <summary>
        ///     Gets or sets the type of the error.
        /// </summary>
        /// <value>The type of the error.</value>
        [Required]
        [MaxLength(255)]
        [JsonPropertyName("errorType")]
        public string ErrorType { get; set; }

        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [Required]
        [MaxLength(255)]
        [JsonPropertyName("source")]
        public string Source { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [Required]
        [MaxLength(255)]
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the stack trace.
        /// </summary>
        /// <value>The stack trace.</value>
        [Required]
        [JsonPropertyName("stackTrace")]
        public string StackTrace { get; set; }

        /// <summary>
        ///     Gets the create command.
        /// </summary>
        /// <returns>LogItemCommand.</returns>
        public LogItemCommand GetCreateCommand()
        {
            return new LogItemCommand
            {
                ErrorType = ErrorType,
                LogLevel = LogLevel,
                Message = Message,
                OccurredOn = OccurredOn,
                Project = Project,
                Source = Source,
                StackTrace = StackTrace
            };
        }
    }
}