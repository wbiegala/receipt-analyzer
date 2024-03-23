namespace BS.ReceiptAnalyzer.Shared.Storage
{
    public sealed record LocalStorageConfiguration
    {
        public string RootDirectory { get; init; }
    }
}
