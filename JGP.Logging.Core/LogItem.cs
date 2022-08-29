// ***********************************************************************
// Assembly         : JGP.Logging.Core
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="LogItem.cs" company="JGP.Logging.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Core
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Commands;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Class LogItem.
    /// </summary>
    public class LogItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LogItem" /> class.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected LogItem()
        {
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogItem" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">command</exception>
        public LogItem(LogItemCommand? command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            LogId = Guid.NewGuid();
            LogLevel = command.LogLevel;
            OccurredOn = command.OccurredOn;
            Project = command.Project;
            ErrorType = command.ErrorType;
            Source = command.Source;
            Message = command.Message;
            StackTrace = command.StackTrace;

            if (command.AdditionalInfo != null)
            {
                AdditionalInfo = new AdditionalInfo(command.AdditionalInfo);
            }
        }

        /// <summary>
        ///     Gets or sets the log identifier.
        /// </summary>
        /// <value>The log identifier.</value>
        public Guid LogId { get; protected set; }

        /// <summary>
        ///     Gets or sets the log level.
        /// </summary>
        /// <value>The log level.</value>
        public LogLevel LogLevel { get; protected set; }

        /// <summary>
        ///     Gets or sets the occurred on.
        /// </summary>
        /// <value>The occurred on.</value>
        public DateTimeOffset OccurredOn { get; protected set; }

        /// <summary>
        ///     Gets or sets the project.
        /// </summary>
        /// <value>The project.</value>
        public string Project { get; protected set; }

        /// <summary>
        ///     Gets or sets the type of the error.
        /// </summary>
        /// <value>The type of the error.</value>
        public string ErrorType { get; protected set; }

        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; protected set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; protected set; }

        /// <summary>
        ///     Gets or sets the stack trace.
        /// </summary>
        /// <value>The stack trace.</value>
        public string StackTrace { get; protected set; }

        /// <summary>
        ///     Gets or sets the additional information.
        /// </summary>
        /// <value>The additional information.</value>
        public AdditionalInfo? AdditionalInfo { get; set; }

        #region OVERRIDES & ESSENTIALS

        /// <summary>
        ///     Equalses the specified log item.
        /// </summary>
        /// <param name="logItem">The log item.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(LogItem? logItem)
        {
            if (logItem is null) return false;
            return LogId == logItem.LogId
                   && LogLevel == logItem.LogLevel
                   && OccurredOn == logItem.OccurredOn
                   && Project == logItem.Project
                   && ErrorType == logItem.ErrorType
                   && Source == logItem.Source
                   && Message == logItem.Message
                   && StackTrace == logItem.StackTrace;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return obj is LogItem logItem
                   && Equals(logItem);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return LogId.GetHashCode();
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            return JsonSerializer.Serialize(this, options);
        }

        #endregion
    }
}