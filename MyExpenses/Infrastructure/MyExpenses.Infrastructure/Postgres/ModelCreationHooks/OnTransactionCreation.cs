using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Expenses;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnTransactionCreation
    {
        public static ModelBuilder OnTransactionCreating(this ModelBuilder modelBuilder)
        {
            //modelBuilder.OnExpenseBaseEntityCreating();
            modelBuilder.OnAuditableEntityCreating<Transaction>();


            modelBuilder.Entity<Transaction>()
                        .Property(pe => pe.TransactionType).IsRequired();
            modelBuilder.Entity<Transaction>()
                        .Property(e => e.Amount).IsRequired();
            modelBuilder.Entity<Transaction>()
                        .Property(e => e.Date).IsRequired();
            
            modelBuilder.Entity<Transaction>()
                        .HasOne(pe=>pe.Account)
                        .WithMany(p=>p.PersonalExpenses)
                        .HasForeignKey(pe => pe.AccountId);


            return modelBuilder;
        }
    }
}
