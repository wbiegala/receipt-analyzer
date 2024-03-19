using BS.ReceiptAnalyzer.Supervisor.Authorization.ApiKeyProviders;
using Defaults = BS.ReceiptAnalyzer.Supervisor.Authorization.ApiKeyAuthorizationDefaults;

namespace BS.ReceiptAnalyzer.Supervisor.Authorization
{
    public class ApiKeyAuthorizationService : IApiKeyAuthorizationService
    {
        private readonly IApiKeyProvider _apiKeyProvider;

        public ApiKeyAuthorizationService(IApiKeyProvider apiKeyProvider)
        {
            _apiKeyProvider = apiKeyProvider ?? throw new ArgumentNullException(nameof(apiKeyProvider));
        }

        public async Task<bool> AuthorizeAsync(string? apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey) || apiKey.Length != Defaults.KeyLenght)
                return false;

            var keys = await _apiKeyProvider.GetValidKeysAsync();

            return keys.Any(key => key.Equals(apiKey));
        }
    }
}
