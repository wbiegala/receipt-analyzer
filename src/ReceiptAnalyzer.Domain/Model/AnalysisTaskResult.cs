using BS.ReceiptAnalyzer.Domain.Basic;
using BS.ReceiptAnalyzer.Domain.Model.ReceiptAnalyzed;

namespace BS.ReceiptAnalyzer.Domain.Model
{
    public record class AnalysisTaskResult : ValueObject
    {
        public IEnumerable<Receipt> Receipts { get; init; } = Enumerable.Empty<Receipt>();
    }
}
