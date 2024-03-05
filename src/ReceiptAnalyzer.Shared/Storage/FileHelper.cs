namespace BS.ReceiptAnalyzer.Shared.Storage
{
    public static class FileHelper
    {
        public static byte[] GetBytes(Stream stream)
        {
            var ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Position = 0;
            using var reader = new BinaryReader(ms);

            return reader.ReadBytes((int)ms.Length);            
        }
    }
}
