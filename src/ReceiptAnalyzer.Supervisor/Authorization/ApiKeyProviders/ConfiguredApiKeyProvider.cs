
namespace BS.ReceiptAnalyzer.Supervisor.Authorization.ApiKeyProviders
{
    internal class ConfiguredApiKeyProvider : IApiKeyProvider
    {
        private readonly IEnumerable<string> _apiKeys;

        public ConfiguredApiKeyProvider(ValidApiKeys validKeys)
        {
            _apiKeys = validKeys.Keys;
        }

        public Task<IEnumerable<string>> GetValidKeysAsync()
        {
            return Task.FromResult(_apiKeys);
        }
    }

    public record ValidApiKeys
    {
        public IEnumerable<string> Keys { get; init; }
    }
}
