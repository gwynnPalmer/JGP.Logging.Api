// ***********************************************************************
// Assembly         : JGP.Logging.Web.Proxy
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="ILoggingApiHelper.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Web.Proxy;

using JGP.Core.Services;
using Models;

/// <summary>
///     Interface ILoggingApiHelper
/// Implements the <see cref="System.IDisposable" />
/// </summary>
/// <seealso cref="System.IDisposable" />
public interface ILoggingApiHelper : IDisposable
{
    /// <summary>
    ///     Get log item as an asynchronous operation.
    /// </summary>
    /// <param name="logId">The log identifier.</param>
    /// <returns>A Task&lt;LogItemModel&gt; representing the asynchronous operation.</returns>
    Task<LogItemModel?> GetLogItemAsync(Guid logId);

    /// <summary>
    ///     Get log items as an asynchronous operation.
    /// </summary>
    /// <param name="project">The project.</param>
    /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
    Task<List<LogItemModel>?> GetLogItemsAsync(string project);

    /// <summary>
    ///     Log as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task&lt;ActionReceipt&gt; representing the asynchronous operation.</returns>
    Task<ActionReceipt?> LogAsync(LogItemModel model);
}