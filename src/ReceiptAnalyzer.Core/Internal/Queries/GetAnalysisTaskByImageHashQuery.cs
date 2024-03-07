using BS.ReceiptAnalyzer.Domain.Model;
using MediatR;

namespace BS.ReceiptAnalyzer.Core.Internal.Queries
{
    internal sealed record GetAnalysisTaskByImageHashQuery(string ImageHash) : IRequest<AnalysisTask?>;
}
