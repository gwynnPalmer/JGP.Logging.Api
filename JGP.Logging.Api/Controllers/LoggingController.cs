// ***********************************************************************
// Assembly         : JGP.Logging.Api
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="LoggingController.cs" company="JGP.Logging.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Api.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using JGP.Core.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Web.Models;

    /// <summary>
    ///     Class LoggingController.
    ///     Implements the <see cref="ControllerBase" />
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/log")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public class LoggingController : ControllerBase
    {
        /// <summary>
        ///     The logging service
        /// </summary>
        private readonly ILoggingService _loggingService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        public LoggingController(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }


        /// <summary>
        ///     Logs the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>IActionResult.</returns>
        [ProducesResponseType(typeof(ActionReceipt), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionReceipt), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionReceipt), StatusCodes.Status404NotFound)]
        [HttpPost("logerror")]
        public async Task<IActionResult> Log([Required] LogItemModel model)
        {
            var receipt = await _loggingService.CreateLogItemAsync(model.GetCreateCommand());
            return receipt.Outcome switch
            {
                ActionOutcome.Exception => BadRequest(receipt),
                ActionOutcome.NotFound => NotFound(receipt),
                ActionOutcome.Success => Ok(receipt),
                _ => throw new ArgumentOutOfRangeException(nameof(receipt.Outcome))
            };
        }

        /// <summary>
        ///     Gets the log item.
        /// </summary>
        /// <param name="logId">The log identifier.</param>
        /// <returns>IActionResult.</returns>
        [ProducesResponseType(typeof(LogItemModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("getlogitem/{logId:guid}")]
        public async Task<IActionResult> GetLogItem([Required] Guid logId)
        {
            var logItem = await _loggingService.GetLogItemAsync(logId);
            if (logItem == null) return NotFound();

            var model = new LogItemModel(logItem);
            return Ok(model);
        }

        /// <summary>
        ///     Gets the log items.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>IActionResult.</returns>
        [ProducesResponseType(typeof(List<LogItemModel>), StatusCodes.Status200OK)]
        [HttpGet("getlogitem/{project}")]
        public async Task<IActionResult> GetLogItems([Required] string project)
        {
            var logItems = await _loggingService.GetLogItemsAsync(project);

            var list = logItems
                .Select(item => new LogItemModel(item))
                .ToList();

            return Ok(list);
        }

        /// <summary>
        ///     Throws this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("throw")]
        public IActionResult Throw()
        {
            _loggingService.Throw();
            return BadRequest();
        }
    }
}