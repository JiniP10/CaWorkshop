using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Kanban.Entities;
using CaWorkshop.Domain.Kanban.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaWorkshop.Infrastructure.Persistence.Configurations;

public class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .HasConversion(
                title => title.Value,
                value => ColumnTitle.Create(value))
            .IsRequired();

        builder.Property(c => c.BoardId)
            .IsRequired();

        // Add an index on the BoardId to optimize queries that filter or join by board (not necessary, supported by convention)
        builder.HasIndex(c => c.BoardId);

        builder.Navigation(c => c.Cards)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(c => c.Cards)
            .WithOne()
            .HasForeignKey(c => c.ColumnId)
            .OnDelete(DeleteBehavior.Cascade); // Ensure all cards are deleted with the column
    }
}
