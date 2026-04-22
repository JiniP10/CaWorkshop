using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Kanban.Entities;
using CaWorkshop.Domain.Kanban.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaWorkshop.Infrastructure.Persistence.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.Property(c => c.Title)
            .HasConversion(
                title => title.Value,
                value => CardTitle.Create(value))
            .IsRequired();
    }
}