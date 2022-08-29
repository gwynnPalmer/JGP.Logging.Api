using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace JGP.Logging.Api.Application.Configuration
{
    /// <summary>
    ///     Class SwaggerConfiguration.
    /// </summary>
    internal static class SwaggerConfiguration
    {
        /// <summary>
        ///     Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BigReference Logging", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.UseInlineDefinitionsForEnums();
                options.CustomSchemaIds(SchemaIdStrategy);

                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                options.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out var methodInfo))
                        return false;

                    var versions = methodInfo
                        .DeclaringType?
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    var maps = methodInfo
                        .GetCustomAttributes(true)
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToList();

                    return versions?.Any(v => $"v{v}" == version) == true
                           && (!maps.Any() || maps.Any(v => $"v{v}" == version));
                });
            });
        }

        /// <summary>
        ///     Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public static void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BigReference Logging V1");
                    c.RoutePrefix = string.Empty;
                    c.SupportedSubmitMethods();
                });
            }
            else
            {
                app.UseSwagger(x =>
                {
                    x.RouteTemplate = "/{documentName}/swagger.json";
                    x.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = env.IsProduction()
                            ? new List<OpenApiServer>
                                { new() { Url = $"{httpReq.Scheme}://api.bigreference.com/logging" } }
                            : new List<OpenApiServer>
                            {
                                new() { Url = $"{httpReq.Scheme}://staging-api.bigreference.com/logging" }
                            };
                    });
                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "BigReference Logging V1");
                    c.RoutePrefix = string.Empty;
                    c.SupportedSubmitMethods();
                });
            }
        }


        /// <summary>
        ///     Schemas the identifier strategy.
        /// </summary>
        /// <param name="currentClass">The current class.</param>
        /// <returns>System.String.</returns>
        private static string SchemaIdStrategy(Type currentClass)
        {
            var returnedValue = currentClass.Name;
            if (returnedValue.Contains("Model"))
                returnedValue = returnedValue.Replace("Model", string.Empty);
            if (returnedValue.Contains("Dto"))
                returnedValue = returnedValue.Replace("Dto", string.Empty);
            return returnedValue;
        }
    }


    /// <summary>
    ///     Class RemoveVersionFromParameter.
    ///     Implements the <see cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
    internal class RemoveVersionFromParameter : IOperationFilter
    {
        /// <summary>
        ///     Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!operation.Parameters.Any())
                return;

            var versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }

    /// <summary>
    ///     Class ReplaceVersionWithExactValueInPath.
    ///     Implements the <see cref="Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter" />
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter" />
    internal class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        /// <summary>
        ///     Applies the specified swagger document.
        /// </summary>
        /// <param name="swaggerDoc">The swagger document.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();

            foreach (var (key, value) in swaggerDoc.Paths)
                paths.Add(key.Replace("v{version}", swaggerDoc.Info.Version), value);

            swaggerDoc.Paths = paths;
        }
    }
}
