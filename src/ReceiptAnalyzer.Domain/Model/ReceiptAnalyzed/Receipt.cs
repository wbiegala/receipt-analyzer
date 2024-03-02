using BS.ReceiptAnalyzer.Domain.Basic;

namespace BS.ReceiptAnalyzer.Domain.Model.ReceiptAnalyzed
{
    public sealed record Receipt : ValueObject
    {
        public Header Header { get; set; }
        public IEnumerable<LineItem> Items { get; set; }
        public SaleSummary SaleSummary { get; set; }
        public Footer Footer { get; set; }
    }
}
