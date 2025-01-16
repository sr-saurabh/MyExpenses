using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.User;

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

            modelBuilder.Entity<AppUser>().HasOne(user => user.UserIdentity).WithOne().HasForeignKey<AppUser>(user => user.UserId).OnDelete(DeleteBehavior.Restrict);

            return modelBuilder;
        }
    }
}
