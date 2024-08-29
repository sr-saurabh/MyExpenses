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
    public static class OnPersonalExpenseCreation
    {
        public static ModelBuilder OnPersonalExpenseCreating(this ModelBuilder modelBuilder)
        {
            //modelBuilder.OnExpenseBaseEntityCreating();
            modelBuilder.OnAuditableEntityCreating<PersonalExpenses>();


            modelBuilder.Entity<PersonalExpenses>()
                        .Property(pe => pe.Type).IsRequired();
            modelBuilder.Entity<PersonalExpenses>()
                        .Property(e => e.Description).IsRequired();
            modelBuilder.Entity<PersonalExpenses>()
                        .Property(e => e.Amount).IsRequired();
            modelBuilder.Entity<PersonalExpenses>()
                        .Property(e => e.Category).IsRequired();
            modelBuilder.Entity<PersonalExpenses>()
                        .Property(e => e.Date).IsRequired();

            modelBuilder.Entity<PersonalExpenses>()
                        .HasOne(pe=>pe.User)
                        .WithMany(p=>p.PersonalExpenses)
                        .HasForeignKey(pe => pe.AppUserId);


            return modelBuilder;
        }
    }
}
