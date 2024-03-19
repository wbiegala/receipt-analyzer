using BS.ReceiptAnalyzer.Supervisor.Authorization.ApiKeyProviders;

namespace BS.ReceiptAnalyzer.Supervisor.Authorization
{
    public static class ApiKeyAuthorizationDefaults
    {
        public const string ApiKeyHeader = "Authorization";
        public const string Prefix = "ApiKey";
        public const int KeyLenght = 64;
    }

    public static class ApiKeyAuthorizationInstaller
    {
        public static IServiceCollection AddApiKeyAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var keys = configuration.GetSection("Auth:Keys").Get<string[]>();
            var apiKeysConfig = new ValidApiKeys { Keys = keys ?? [] };
            services.AddSingleton(apiKeysConfig);

            services.AddScoped<IApiKeyProvider, ConfiguredApiKeyProvider>();
            services.AddScoped<IApiKeyAuthorizationService, ApiKeyAuthorizationService>();

            services.AddScoped<AuthorizationMiddleware>();

            return services;
        }

        public static void UseApiKeyAuth(this WebApplication app)
        {
            app.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
