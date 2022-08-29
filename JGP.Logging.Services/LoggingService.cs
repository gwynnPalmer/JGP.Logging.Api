// ***********************************************************************
// Assembly         : JGP.Logging.Services
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="LoggingService.cs" company="JGP.Logging.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Services
{
    using System.Diagnostics;
    using Core;
    using Core.Commands;
    using Data.EntityFramework;
    using JGP.Core.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Class LoggingService.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        /// <summary>
        ///     The log context
        /// </summary>
        private readonly ILogContext _logContext;

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger<LoggingService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="logContext">The log context.</param>
        public LoggingService(ILogger<LoggingService> logger, ILogContext logContext)
        {
            _logger = logger;
            _logContext = logContext;
        }

        #region DISPOSAL

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            _logContext.Dispose();
        }

        #endregion

        /// <summary>
        ///     Create log item as an asynchronous operation.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A Task&lt;ActionReceipt&gt; representing the asynchronous operation.</returns>
        public async Task<ActionReceipt> CreateLogItemAsync(LogItemCommand command)
        {
            try
            {
                var logItem = new LogItem(command);
                _logContext.LogItems.Add(logItem);

                var affectedTotal = await _logContext.SaveChangesAsync();
                return ActionReceipt.GetSuccessReceipt(affectedTotal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create Log");
                return ActionReceipt.GetErrorReceipt(ex);
            }
        }

        /// <summary>
        ///     Get log item as an asynchronous operation.
        /// </summary>
        /// <param name="logId">The log identifier.</param>
        /// <returns>A Task&lt;LogItem&gt; representing the asynchronous operation.</returns>
        public async Task<LogItem?> GetLogItemAsync(Guid logId)
        {
            try
            {
                return await _logContext.LogItems
                    .AsNoTracking()
                    .FirstOrDefaultAsync(item => item.LogId == logId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get Log for Log ID: {logId}", logId);
                return null;
            }
        }

        /// <summary>
        ///     Get log items as an asynchronous operation.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<LogItem>> GetLogItemsAsync(string project)
        {
            try
            {
                return await _logContext.LogItems
                    .AsNoTracking()
                    .Where(item => item.Project == project)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get Logs for Project {project}", project);
                return new List<LogItem>();
            }
        }

        public void Throw()
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation("I'm about to throw an exception!");
                throw new NotImplementedException("Here's an exception message!", new Exception("I'm another exception!"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Threw test exception");
            }
            stopWatch.Stop();
            _logger.LogInformation($"Logging Task took {stopWatch.ElapsedMilliseconds}ms");
        }
    }
}