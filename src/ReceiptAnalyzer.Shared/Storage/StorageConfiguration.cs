namespace BS.ReceiptAnalyzer.Shared.Storage
{
    public sealed record StorageConfiguration
    {
        public const string DefaultContainerName = "receiptanalyzer";

        public string ConnectionString { get; init; }
        public string ContainerName { get; init; } = DefaultContainerName;
    }
}
