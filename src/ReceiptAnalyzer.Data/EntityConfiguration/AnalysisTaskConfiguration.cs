using BS.ReceiptAnalyzer.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace BS.ReceiptAnalyzer.Data.EntityConfiguration
{
    internal class AnalysisTaskConfiguration : IEntityTypeConfiguration<AnalysisTask>
    {
        public void Configure(EntityTypeBuilder<AnalysisTask> builder)
        {
            builder.ToTable(nameof(AnalysisTask));

            builder.HasKey(at => at.Id);

            builder.Ignore(at => at.DomainEvents);

            builder.Property(at => at.ImageHash);

            builder.Property(at => at.CreationTime)
                .IsRequired();

            builder.Property(at => at.StartTime);

            builder.Property(at => at.EndTime);

            builder.Property(at => at.Status)
                .HasConversion<string>();

            builder.Property(at => at.Progression)
                .HasConversion<string>();

            builder.HasMany(at => at.ProgressionDetails)
                .WithOne()
                .HasForeignKey("AnalysisTaskId");

            builder.Property(at => at.Results)
                .HasConversion(
                    val => JsonSerializer.Serialize(val, JsonSerializerOptions.Default),
                    val => JsonSerializer.Deserialize<AnalysisTaskResult>(val, JsonSerializerOptions.Default));

            builder.Property(at => at.FailReason);


            builder.HasIndex(at => at.ImageHash);
        }
    }
}
