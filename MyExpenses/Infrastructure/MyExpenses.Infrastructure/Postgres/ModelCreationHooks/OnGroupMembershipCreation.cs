using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnGroupMembershipCreation
    {
        public static ModelBuilder OnGroupMemberShipCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<UserGroupMembership>();
            modelBuilder.Entity<UserGroupMembership>()
                .HasOne(gm => gm.AppUser)
                .WithMany(u => u.GroupMemberships)
                .HasForeignKey(gm => gm.AppUserId);

            modelBuilder.Entity<UserGroupMembership>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.GroupMemberships)
                .HasForeignKey(gm => gm.GroupId);
            return modelBuilder;
        }
    }
}
