using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnExpenseBaseEntityCreation
    {
        public static ModelBuilder OnExpenseBaseEntityCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<ExpenseBaseEntity>();
            modelBuilder.Entity<ExpenseBaseEntity>()
                        .Property(e => e.Description).IsRequired();
            modelBuilder.Entity<ExpenseBaseEntity>()
                        .Property(e => e.Amount).IsRequired();
            modelBuilder.Entity<ExpenseBaseEntity>()
                        .Property(e => e.Category).IsRequired();
            modelBuilder.Entity<ExpenseBaseEntity>()
                        .Property(e => e.Date).IsRequired();
            return modelBuilder;
        }
    }
}
