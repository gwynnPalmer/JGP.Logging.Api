// ***********************************************************************
// Assembly         : JGP.Logging.Middleware
// Author           : Joshua Gwynn-Palmer
// Created          : 08-29-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 08-29-2022
// ***********************************************************************
// <copyright file="JGPLogger.cs" company="JGP.Logging.Middleware">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Logging.Middleware
{
    using System.Net;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Core.Builders;
    using JGP.Api.KeyManagement.Authentication;
    using JGP.Core.Serialization;
    using JGP.Core.Services;
    using Microsoft.Extensions.Logging;
    using Web.Models;

    /// <summary>
    ///     Class JGPLogger.
    ///     Implements the <see cref="ILogger" />
    /// </summary>
    /// <seealso cref="ILogger" />
    internal class JGPLogger : ILogger
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
        ///     The log item builder
        /// </summary>
        private readonly LogItemBuilder _logItemBuilder;

        /// <summary>
        ///     Initializes static members of the <see cref="JGPLogger" /> class.
        /// </summary>
        static JGPLogger()
        {
            JsonOptions = new JsonSerializerOptions();
            JsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            JsonOptions.Converters.Add(new ActionReceiptConverter());
            JsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            JsonOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            JsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JGPLogger" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <exception cref="System.InvalidOperationException">No endpoint found for service {ServiceName}</exception>
        public JGPLogger(JGPLoggerProvider provider)
        {
            _logItemBuilder = new LogItemBuilder(provider.Options.ProjectName);

            if (_httpClient != null) return;

            var endpoint = provider.ApiConfiguration.Services
                .FirstOrDefault(service => service.ServiceName == ServiceName);

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
        ///     Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">The type of the state to begin scope for.</typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>An <see cref="T:System.IDisposable" /> that ends the logical operation scope on dispose.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        ///     Checks if the given <paramref name="logLevel" /> is enabled.
        /// </summary>
        /// <param name="logLevel">Level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        /// <summary>
        ///     Writes a log entry.
        /// </summary>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">
        ///     Function to create a <see cref="T:System.String" /> message of the <paramref name="state" />
        ///     and <paramref name="exception" />.
        /// </param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            if (exception == null) return;

            var logItem = _logItemBuilder.BuildLogItem(exception, logLevel);
            LogAsync(new LogItemModel(logItem)).GetAwaiter().GetResult();
        }

        /// <summary>
        ///     Log as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;ActionReceipt&gt; representing the asynchronous operation.</returns>
        private async Task<ActionReceipt?> LogAsync(LogItemModel model)
        {
            var response = await SendPostMessageAsync(model, $"{LoggingPath}/logerror");
            return await response.Content.ReadFromJsonAsync<ActionReceipt>(JsonOptions);
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
                //_logger.LogError(ex, "Could not fulfill POST request at route: {route}", route);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}