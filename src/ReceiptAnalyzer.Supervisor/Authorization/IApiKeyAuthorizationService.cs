namespace BS.ReceiptAnalyzer.Supervisor.Authorization
{
    public interface IApiKeyAuthorizationService
    {
        Task<bool> AuthorizeAsync(string? apiKey);
    }
}
