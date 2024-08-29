using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Settlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnSettlementCreation
    {
        public static ModelBuilder OnSettlementCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<SettlementHistory>();

            modelBuilder.Entity<SettlementHistory>()
                        .HasOne(s => s.FromUser)
                        .WithMany(u => u.FromSettlements)
                        .HasForeignKey(s => s.FromUserId);
            
            modelBuilder.Entity<SettlementHistory>()
                        .HasOne(s => s.ToUser)
                        .WithMany(u => u.ToSettlements)
                        .HasForeignKey(s => s.ToUserId);
            
            modelBuilder.Entity<SettlementHistory>()
                        .HasOne(s => s.Group)
                        .WithMany(g => g.SettlementHistories)
                        .HasForeignKey(s => s.GroupId);


            return modelBuilder;
        }
    }
}
