using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static void SetAuditProperties(this ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();

            IEnumerable<EntityEntry> entities = changeTracker.Entries().Where(t => t.Entity is IEntityBase && (t.State == EntityState.Added || t.State == EntityState.Modified));

            if (entities.Any())
            {
                var timestamp = DateTimeOffset.Now;

                foreach (EntityEntry entry in entities)
                {
                    var entity = (IEntityBase)entry.Entity;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedOn = timestamp;
                            entity.UpdatedOn = timestamp;
                            break;
                        case EntityState.Modified:
                            entity.UpdatedOn = timestamp;
                            break;
                    }
                }
            }
        }
    }
}
