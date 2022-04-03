using Microsoft.EntityFrameworkCore;
using Survey.Entities;
using System;
using System.Linq;

namespace Survey.DataAccess
{
    public static class ModelBuilderExtensions
    {
        public static void AddAuditableProperites(this ModelBuilder modelBuilder)
        {
            var entities = modelBuilder
                .Model
                .GetEntityTypes()
                .Where(x => typeof(IAuditable).IsAssignableFrom(x.ClrType));

            foreach (var entity in entities)
            {
                modelBuilder.Entity(entity.ClrType).Property<DateTime>("CreatedAt");
                modelBuilder.Entity(entity.ClrType).Property<DateTime?>("ModifiedAt");
            }
        }
    }
}
