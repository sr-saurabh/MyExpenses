using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnGoalCreation
    {
        public static ModelBuilder OnGoalCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<AppUser>();

            modelBuilder.Entity<Goal>()
                        .HasOne(pe => pe.AppUser)
                        .WithMany(p => p.Goals)
                        .HasForeignKey(pe => pe.AppUserId);

            return modelBuilder;
        }
    }
}
