namespace JGP.Logging.Api.Application.Configuration
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Class MigrationConfiguration.
    /// </summary>
    internal static class MigrationConfiguration
    {
        /// <summary>
        ///     Ensures the migration of context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app">The application.</param>
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<T>();
            context?.Database.Migrate();
        }
    }
}
