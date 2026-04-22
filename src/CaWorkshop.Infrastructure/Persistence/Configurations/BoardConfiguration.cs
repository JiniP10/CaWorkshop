using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Kanban.Entities;
using CaWorkshop.Domain.Kanban.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaWorkshop.Infrastructure.Persistence.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        // Explicitly set the primary key (not necessary, supported by convention)
        builder.HasKey(b => b.Id);

        // Configure the BoardTitle value object using a simple conversion
        builder.Property(b => b.Title)
            .HasConversion(
                title => title.Value,               // to the database
                value => BoardTitle.Create(value))  // from the database
            .IsRequired();                          // make the column non-nullable

        // Use field access for the Columns collection (_columns)
        builder.Navigation(b => b.Columns)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // Define the one-to-many relationship: Board has many Columns
        builder.HasMany(b => b.Columns)
            .WithOne()                              // inverse navigation not needed
            .HasForeignKey(c => c.BoardId)          // link via BoardId on Column
            .OnDelete(DeleteBehavior.Cascade);      // delete columns when board is deleted
    }
}