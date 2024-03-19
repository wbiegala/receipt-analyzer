using Defaults = BS.ReceiptAnalyzer.Supervisor.Authorization.ApiKeyAuthorizationDefaults;

namespace BS.ReceiptAnalyzer.Supervisor.Authorization
{
    public static class ApiKeyExtensions
    {
        public static string? GetApiKey(this HttpContext httpContext)
        {
            var header = httpContext.Request.Headers[Defaults.ApiKeyHeader].ToString();
            
            if (string.IsNullOrWhiteSpace(header))
                return null;

            if (header.Length != (Defaults.KeyLenght + 1 + Defaults.Prefix.Length))
                return null;

            if (!header.StartsWith(Defaults.Prefix))
                return null;

            return header.Substring(Defaults.Prefix.Length + 1, Defaults.KeyLenght);
        }
    }
}
