using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnAppUserCreation
    {
        public static ModelBuilder OnAppUserCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<AppUser>();
            modelBuilder.Entity<AppUser>()
                        .Property(user => user.Id)
                        .IsRequired();

            //modelBuilder.Entity<AppUser>()
            //            .Property(user => user.FullName)
            //            .HasComputedColumnSql("FirstName || ' ' || LastName", true);

            //modelBuilder.Entity<AppUser>()
            //    .HasMany(a => a.Contacts)
            //    .WithOne()
            //    .HasForeignKey(c => c.FromUserId);

            return modelBuilder;
        }
    }
}
