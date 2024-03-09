using BS.ReceiptAnalyzer.Domain.Basic;
using BS.ReceiptAnalyzer.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BS.ReceiptAnalyzer.Data
{
    public class ReceiptAnalyzerDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<AnalysisTask> Tasks { get; set; }

        public ReceiptAnalyzerDbContext(IMediator mediator) : base()
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public ReceiptAnalyzerDbContext(IMediator mediator, DbContextOptions options) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReceiptAnalyzerDbContext).Assembly);
        }

        public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            var aggregateType = typeof(AggregateRoot);
            foreach (var entry in ChangeTracker.Entries())
            {
                if (aggregateType.IsAssignableFrom(entry.Entity.GetType()))
                {
                    var entity = entry.Entity as AggregateRoot;
                    var events = entity?.DomainEvents;

                    if (events != null && events.Any())
                    {
                        await Task.WhenAll(events.Select(@event => _mediator.Publish(@event, cancellationToken)));
                        entity?.ClearEvents();
                    }
                }
            }

            await base.SaveChangesAsync(cancellationToken);
        }
    }
}
