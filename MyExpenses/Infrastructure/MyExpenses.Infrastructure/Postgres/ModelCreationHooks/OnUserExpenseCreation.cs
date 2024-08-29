using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Entities.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnUserExpenseCreation
    {
        public static ModelBuilder OnUserExpenseCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<UserExpense>();
            modelBuilder.Entity<UserExpense>()
                        .Property(e => e.Description).IsRequired();
            modelBuilder.Entity<UserExpense>()
                        .Property(e => e.Amount).IsRequired();
            modelBuilder.Entity<UserExpense>()
                        .Property(e => e.Category).IsRequired();
            modelBuilder.Entity<UserExpense>()
                        .Property(e => e.Date).IsRequired();

            modelBuilder.Entity<UserExpense>()
               .HasOne(ue => ue.FromUser)
               .WithMany(u => u.FromUserExpenses)
               .HasForeignKey(ue => ue.FromUserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserExpense>()
               .HasOne(ue => ue.ToUser)
               .WithMany(u => u.ToUserExpenses)
               .HasForeignKey(ue => ue.ToUserId)
               .OnDelete(DeleteBehavior.Restrict);

            return modelBuilder;
        }

    }
}
