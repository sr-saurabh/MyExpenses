using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnActivityCreation
    {
        public static ModelBuilder OnActivityCreating(this ModelBuilder modelBuilder)
        {
            //modelBuilder.OnExpenseBaseEntityCreating();
            modelBuilder.OnAuditableEntityCreating<Activity>();


            modelBuilder.Entity<Activity>()
                        .Property(pe => pe.CategoryId).IsRequired();

            modelBuilder.Entity<Activity>()
                        .Property(e => e.Description).IsRequired();

            modelBuilder.Entity<Activity>()
                        .HasOne(activity => activity.Transaction)
                        .WithOne(transaction => transaction.Activity).HasForeignKey<Transaction>(a=>a.ActivityId);
            
            modelBuilder.Entity<Activity>()
                        .HasOne(activity => activity.User)
                        .WithMany(user => user.Activities)
                        .HasForeignKey(activity=>activity.AppUserId);


            return modelBuilder;
        }
    }
}
