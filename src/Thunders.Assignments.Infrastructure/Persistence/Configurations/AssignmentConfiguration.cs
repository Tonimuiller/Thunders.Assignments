using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.Assignments.Domain.Entities;

namespace Thunders.Assignments.Infrastructure.Persistence.Configurations;

internal sealed class AssignmentConfiguration : IEntityTypeConfiguration<Domain.Entities.Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.ToTable("Assignments");
        builder.HasKey(a => a.Id);
        builder
            .Property(a => a.CreatedAt)
            .IsRequired();
        builder
            .Property(a => a.UpdatedAt)
            .IsRequired(false);
        builder
            .Property(a => a.Title)
            .IsRequired();
        builder
            .Property(a => a.Description)
            .IsRequired(false);
        builder
            .Property(a => a.Done)
            .IsRequired();
        builder
            .Property(a => a.Schedule)
            .IsRequired();
    }
}
