namespace BS.ReceiptAnalyzer.Supervisor.Authorization.ApiKeyProviders
{
    public interface IApiKeyProvider
    {
        Task<IEnumerable<string>> GetValidKeysAsync();
    }
}
