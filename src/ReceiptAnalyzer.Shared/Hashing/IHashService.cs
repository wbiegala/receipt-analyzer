namespace BS.ReceiptAnalyzer.Shared.Hashing
{
    public interface IHashService
    {
        string GetHash(string data);
        string GetHash(byte[] data);
    }
}
