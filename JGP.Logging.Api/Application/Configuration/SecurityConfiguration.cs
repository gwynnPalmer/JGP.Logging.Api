namespace JGP.Logging.Api.Application.Configuration
{
    using JGP.Api.KeyManagement.Authentication;

    /// <summary>
    ///     Class SecurityConfiguration.
    /// </summary>
    internal static class SecurityConfiguration
    {
        /// <summary>
        ///     Configures the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="System.InvalidOperationException">No {nameof(ApiKeyAuthenticationSettings)} found</exception>
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.BuildServiceProvider().GetService<ApiKeyAuthenticationSettings>();
            if (settings == null)
            {
                throw new InvalidOperationException($"No {nameof(ApiKeyAuthenticationSettings)} found");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationSettings.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationSettings.DefaultScheme;
            }).AddApiKeyManagement(settings);
        }
    }
}
