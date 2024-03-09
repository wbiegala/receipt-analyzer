using BS.ReceiptAnalyzer.Domain.Model;

namespace BS.ReceiptAnalyzer.Data
{
    public interface IAnalysisTaskRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<AnalysisTask?> GetById(Guid id);
        Task<AnalysisTask?> GetByImageHash(string imageHash);
        Task Add(AnalysisTask entity);
    }
}
