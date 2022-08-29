// ***********************************************************************
// Assembly         : JGP.Logging.Web.Proxy
// Author           : Joshua Gwynn-Palmer
// Created          : 08-28-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-28-2022
// ***********************************************************************
// <copyright file="LoggingApiHelper.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Web.Proxy
{
    using System.Net;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Api.KeyManagement.Authentication;
    using JGP.Core.Serialization;
    using JGP.Core.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models;

    /// <summary>
    ///     Class LoggingApiHelper.
    /// </summary>
    public class LoggingApiHelper : ILoggingApiHelper
    {
        /// <summary>
        ///     The logging path
        /// </summary>
        private const string LoggingPath = "v1/log";

        /// <summary>
        ///     The service name
        /// </summary>
        private const string ServiceName = "Logging";

        /// <summary>
        ///     The json options
        /// </summary>
        private static readonly JsonSerializerOptions? JsonOptions;

        /// <summary>
        ///     The HTTP client
        /// </summary>
        private static HttpClient? _httpClient;

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger<LoggingApiHelper> _logger;

        /// <summary>
        ///     Initializes static members of the <see cref="LoggingApiHelper" /> class.
        /// </summary>
        static LoggingApiHelper()
        {
            JsonOptions = new JsonSerializerOptions();
            JsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            JsonOptions.Converters.Add(new ActionReceiptConverter());
            JsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            JsonOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            JsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingApiHelper" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="apiOptions">The API options.</param>
        /// <exception cref="System.InvalidOperationException">No endpoint found for service {ServiceName}</exception>
        public LoggingApiHelper(ILogger<LoggingApiHelper> logger, IOptions<ApiConfiguration> apiOptions)
        {
            _logger = logger;

            if (_httpClient != null) return;

            var config = apiOptions.Value;
            var endpoint = config.Services.FirstOrDefault(service => service.ServiceName == ServiceName);
            if (endpoint == null)
            {
                throw new InvalidOperationException($"No endpoint found for service {ServiceName}");
            }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(endpoint.Url!)
            };
            _httpClient.DefaultRequestHeaders.Add(ApiKeyConstants.ApiKeyHeaderName, endpoint.ApiKey);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingApiHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="url">The URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <exception cref="System.ArgumentNullException">url</exception>
        /// <exception cref="System.ArgumentNullException">apiKey</exception>
        private LoggingApiHelper(ILogger<LoggingApiHelper> logger, string url, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));

            _logger = logger;

            if (_httpClient != null) return;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            _httpClient.DefaultRequestHeaders.Add(ApiKeyConstants.ApiKeyHeaderName, apiKey);
        }

        /// <summary>
        ///     Gets the API helper.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="url">The URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns>LoggingApiHelper.</returns>
        public static LoggingApiHelper GetApiHelper(ILogger<LoggingApiHelper> logger, string url, string apiKey)
        {
            return new LoggingApiHelper(logger, url, apiKey);
        }

        #region DISPOSAL

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        #endregion

        /// <summary>
        ///     Get log item as an asynchronous operation.
        /// </summary>
        /// <param name="logId">The log identifier.</param>
        /// <returns>A Task&lt;LogItemModel&gt; representing the asynchronous operation.</returns>
        public async Task<LogItemModel?> GetLogItemAsync(Guid logId)
        {
            var response = await SendGetMessageAsync($"{LoggingPath}/getlogitem/{logId}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<LogItemModel>(JsonOptions);
        }

        /// <summary>
        ///     Get log items as an asynchronous operation.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<LogItemModel>?> GetLogItemsAsync(string project)
        {
            var response = await SendGetMessageAsync($"{LoggingPath}/getlogitems/{project}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<List<LogItemModel>>(JsonOptions);
        }

        /// <summary>
        ///     Log as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;ActionReceipt&gt; representing the asynchronous operation.</returns>
        public async Task<ActionReceipt?> LogAsync(LogItemModel model)
        {
            var response = await SendPostMessageAsync(model, $"{LoggingPath}/logerror");
            return await response.Content.ReadFromJsonAsync<ActionReceipt>(JsonOptions);
        }

        #region HELPER METHODS

        /// <summary>
        ///     send delete message as an asynchronous operation.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        private async Task<HttpResponseMessage> SendDeleteMessageAsync(string route)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, route);
                return await _httpClient!.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not fulfill DELETE request at route: {route}", route);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     send get message as an asynchronous operation.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        private async Task<HttpResponseMessage> SendGetMessageAsync(string route)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, route);
                return await _httpClient!.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not fulfill GET request at route: {route}", route);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     send post message as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="route">The route.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        private async Task<HttpResponseMessage> SendPostMessageAsync(object? model, string route)
        {
            try
            {
                if (model == null)
                {
                    return await _httpClient!.SendAsync(new HttpRequestMessage(HttpMethod.Post, route));
                }

                var json = JsonSerializer.Serialize(model);
                var request = new HttpRequestMessage(HttpMethod.Post, route)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/Json")
                };

                return await _httpClient!.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not fulfill POST request at route: {route}", route);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     send put message as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="route">The route.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        private async Task<HttpResponseMessage> SendPutMessageAsync(object? model, string route)
        {
            try
            {
                if (model == null)
                {
                    return await _httpClient!.SendAsync(new HttpRequestMessage(HttpMethod.Put, route));
                }

                var json = JsonSerializer.Serialize(model);
                var request = new HttpRequestMessage(HttpMethod.Put, route)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/Json")
                };

                return await _httpClient!.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not fulfill PUT request at route: {route}", route);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        #endregion
    }
}