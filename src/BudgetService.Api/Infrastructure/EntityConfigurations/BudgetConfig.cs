using BudgetService.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetService.Api.Infrastructure.EntityConfigurations;

public class BudgetConfig : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable(nameof(Budget));

        builder.Property(entity => entity.Id)
            .ValueGeneratedOnAdd();
        
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(entity => entity.Status)
            .IsRequired();
    }
}
