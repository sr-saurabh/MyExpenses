using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnGroupExpenseShareCreation
    {
        public static ModelBuilder OnGroupExpenseShareCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<GroupExpenseShare>();
            modelBuilder.Entity<GroupExpenseShare>()
                        .Property(ges => ges.ShareAmount)
                        .IsRequired();

            modelBuilder.Entity<GroupExpenseShare>()
                        .HasOne(ges => ges.Reciever)
                        .WithMany()
                        .HasForeignKey(ges => ges.ReceiverId);
            
            modelBuilder.Entity<GroupExpenseShare>()
                        .HasOne(ges => ges.GroupExpense)
                        .WithMany(ge=>ge.GroupExpenseShares)
                        .HasForeignKey(ges => ges.GroupExpenseId);


            return modelBuilder;
        }
    }
}
