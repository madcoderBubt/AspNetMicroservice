using Microsoft.EntityFrameworkCore;
using Order.Domain.Common;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Data.Context
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<OrderEntity> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var tracks = ChangeTracker.Entries<EntityBase>();
            foreach (var track in tracks)
            {
                switch (track.State)
                {
                    //case EntityState.Detached:
                    //    break;
                    //case EntityState.Unchanged:
                    //    break;
                    //case EntityState.Deleted:
                    //    break;
                    case EntityState.Modified:
                        track.Entity.UpdatedBy = "testUser";
                        track.Entity.UpdatedDate = DateTime.Now;
                        break;
                    case EntityState.Added:
                        track.Entity.CreatedDate = DateTime.Now;
                        track.Entity.CreatedBy = "testUser";
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
