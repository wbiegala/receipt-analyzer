using BS.ReceiptAnalyzer.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BS.ReceiptAnalyzer.Data.EntityConfiguration
{
    internal class AnalysisTaskStepConfiguration : IEntityTypeConfiguration<AnalysisTaskStep>
    {
        public void Configure(EntityTypeBuilder<AnalysisTaskStep> builder)
        {
            builder.ToTable(nameof(AnalysisTaskStep));

            builder.HasKey(ats => ats.Id);

            builder.Property(ats => ats.StepType)
                .HasConversion<string>();

            builder.Property(ats => ats.Success)
                .IsRequired();

            builder.Property(ats => ats.NotificationTime)
                .IsRequired();

            builder.Property(ats => ats.StartTime)
                .IsRequired();

            builder.Property(ats => ats.EndTime)
                .IsRequired();
        }
    }
}
