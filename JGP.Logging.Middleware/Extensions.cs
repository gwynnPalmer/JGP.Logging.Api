namespace JGP.Logging.Middleware
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class Extensions
    {
        public static ILoggingBuilder AddJGPLogger(this ILoggingBuilder builder, Action<JGPLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, JGPLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}