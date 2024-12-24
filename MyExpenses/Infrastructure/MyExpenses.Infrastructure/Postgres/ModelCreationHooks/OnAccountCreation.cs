using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnAccountCreation
    {
        public static ModelBuilder OnAccountCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<AppUser>();
            modelBuilder.Entity<Account>()
                        .HasOne(account => account.AppUser)
                        .WithMany(user => user.Accounts)
                        .HasForeignKey(account => account.AppUserId);
                        
            return modelBuilder;
        }
    }
}
