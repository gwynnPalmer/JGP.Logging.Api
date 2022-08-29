// ***********************************************************************
// Assembly         : JGP.Logging.NativeLogging
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="LogWriter.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.NativeLogging
{
    using System.Data;
    using Core;
    using Microsoft.Data.SqlClient;

    /// <summary>
    ///     Class LogWriter.
    /// </summary>
    internal class LogWriter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LogWriter" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public LogWriter(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        ///     Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        private string ConnectionString { get; }

        /// <summary>
        ///     Logs the error.
        /// </summary>
        /// <param name="logItem">The log item.</param>
        public void LogError(LogItem logItem)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.Text,
                CommandText =
                    "INSERT INTO dbo.LogItems ([LogId], [OccurredOn], [Project], [ErrorType], [Source], [Message], [StackTrace]) " +
                    "VALUES (@LogId, @OccurredOn, @Project, @ErrorType, @Source, @Message, @StackTrace)",
                Parameters =
                {
                    new SqlParameter("@LogId", logItem.LogId),
                    new SqlParameter("@OccurredOn", logItem.OccurredOn),
                    new SqlParameter("@Project", logItem.Project),
                    new SqlParameter("@ErrorType", logItem.ErrorType),
                    new SqlParameter("@Source", logItem.Source),
                    new SqlParameter("@Message", logItem.Message),
                    new SqlParameter("@StackTrace", logItem.StackTrace)
                }
            };

            Execute(connection, command);

            if (logItem.AdditionalInfo != null)
            {
                LogAdditionalInfo(logItem.AdditionalInfo);
            }
        }

        /// <summary>
        ///     Executes the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="command">The command.</param>
        private static void Execute(IDbConnection connection, IDbCommand command)
        {
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        ///     Logs the additional information.
        /// </summary>
        /// <param name="additionalInfo">The additional information.</param>
        private void LogAdditionalInfo(AdditionalInfo additionalInfo)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.Text,
                CommandText =
                    "INSERT INTO dbo.AdditionalInfo ([LogId], [MachineName], [IpAddress], [OperatingSystem], [OperatingSystemVersion], [Architecture]) " +
                    "VALUES (@LogId, @MachineName, @IpAddress, @OperatingSystem, @OperatingSystemVersion, @Architecture)",
                Parameters =
                {
                    new SqlParameter("@LogId", additionalInfo.LogId),
                    new SqlParameter("@MachineName", additionalInfo.MachineName),
                    new SqlParameter("@IpAddress", additionalInfo.IpAddress),
                    new SqlParameter("@OperatingSystem", additionalInfo.OperatingSystem),
                    new SqlParameter("@OperatingSystemVersion", additionalInfo.OperatingSystemVersion),
                    new SqlParameter("@Architecture", additionalInfo.Architecture)
                }
            };

            Execute(connection, command);
        }
    }
}