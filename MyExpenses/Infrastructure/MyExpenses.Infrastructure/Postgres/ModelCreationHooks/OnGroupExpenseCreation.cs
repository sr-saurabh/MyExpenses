using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnGroupExpenseCreation
    {
        public static ModelBuilder OnGroupExpenseCreating(this ModelBuilder modelBuilder)
        {
            //modelBuilder.OnExpenseBaseEntityCreating();
            modelBuilder.OnAuditableEntityCreating<GroupExpenses>();
            modelBuilder.Entity<GroupExpenses>()
                        .Property(e => e.Description).IsRequired();
            modelBuilder.Entity<GroupExpenses>()
                        .Property(e => e.Amount).IsRequired();
            modelBuilder.Entity<GroupExpenses>()
                        .Property(e => e.Category).IsRequired();
            modelBuilder.Entity<GroupExpenses>()
                        .Property(e => e.Date).IsRequired();

            modelBuilder.Entity<GroupExpenses>()
                        .HasOne(ge => ge.Payer)
                        .WithMany()
                        .HasForeignKey(ge => ge.PayerId);
            
            modelBuilder.Entity<GroupExpenses>()
                        .HasOne(ge => ge.Group)
                        .WithMany(g=>g.GroupExpenses)
                        .HasForeignKey(ge => ge.GroupId);

            return modelBuilder;
        }
    }
}
