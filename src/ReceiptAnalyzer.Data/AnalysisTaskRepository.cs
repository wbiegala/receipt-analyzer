using BS.ReceiptAnalyzer.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BS.ReceiptAnalyzer.Data
{
    internal class AnalysisTaskRepository : IAnalysisTaskRepository
    {
        private readonly ReceiptAnalyzerDbContext _dbContext;

        public AnalysisTaskRepository(ReceiptAnalyzerDbContext dbContext)
        {
            _dbContext = dbContext
                ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public async Task Add(AnalysisTask entity)
        {
            if (entity.Id == default)
            {
                await _dbContext.AddAsync(entity);
            }
        }

        public Task<AnalysisTask?> GetById(Guid id)
        {
            return _dbContext.Tasks
                .Include(task => task.ProgressionDetails)
                .SingleOrDefaultAsync(task => task.Id == id);
        }

        public Task<AnalysisTask?> GetByImageHash(string imageHash)
        {
            return _dbContext.Tasks
                .Include(task => task.ProgressionDetails)
                .OrderByDescending(task => task.StartTime)
                .FirstOrDefaultAsync(task => task.ImageHash == imageHash);
        }        
    }
}
