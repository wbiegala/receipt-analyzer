using BS.ReceiptAnalyzer.Data;
using BS.ReceiptAnalyzer.Domain.Model;
using MediatR;

namespace BS.ReceiptAnalyzer.Core.Internal.Queries
{
    internal class GetAnalysisTaskByImageHashQueryHandler : IRequestHandler<GetAnalysisTaskByImageHashQuery, AnalysisTask?>
    {
        private readonly IAnalysisTaskRepository _repository;

        public GetAnalysisTaskByImageHashQueryHandler(IAnalysisTaskRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<AnalysisTask?> Handle(GetAnalysisTaskByImageHashQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetByImageHash(query.ImageHash);
        }
    }
}
