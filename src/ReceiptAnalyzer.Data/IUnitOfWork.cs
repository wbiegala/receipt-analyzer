namespace BS.ReceiptAnalyzer.Data
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync(CancellationToken cancellationToken = default);
    }
}
