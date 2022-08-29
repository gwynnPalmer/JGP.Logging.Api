namespace JGP.Logging.Services;

using Core;
using Core.Commands;
using JGP.Core.Services;

public interface ILoggingService : IDisposable
{
    /// <summary>
    ///     Create log item as an asynchronous operation.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <returns>A Task&lt;ActionReceipt&gt; representing the asynchronous operation.</returns>
    Task<ActionReceipt> CreateLogItemAsync(LogItemCommand command);

    /// <summary>
    ///     Get log item as an asynchronous operation.
    /// </summary>
    /// <param name="logId">The log identifier.</param>
    /// <returns>A Task&lt;LogItem&gt; representing the asynchronous operation.</returns>
    Task<LogItem?> GetLogItemAsync(Guid logId);

    /// <summary>
    ///     Get log items as an asynchronous operation.
    /// </summary>
    /// <param name="project">The project.</param>
    /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
    Task<List<LogItem>> GetLogItemsAsync(string project);

    /// <summary>
    ///     Throws this instance.
    /// </summary>
    void Throw();
}