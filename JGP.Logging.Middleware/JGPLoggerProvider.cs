namespace JGP.Logging.Middleware
{
    using JGP.Api.KeyManagement.Authentication;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    internal class JGPLoggerProvider : ILoggerProvider
    {
        public readonly ApiConfiguration ApiConfiguration;

        public readonly JGPLoggerOptions Options;

        public JGPLoggerProvider(IOptions<ApiConfiguration> apiConfiguration, IOptions<JGPLoggerOptions> options)
        {
            ApiConfiguration = apiConfiguration.Value;
            Options = options.Value;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new JGPLogger(this);
        }
    }
}