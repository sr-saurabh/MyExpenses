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

            modelBuilder.Entity<AppUser>()
                        .HasOne(user => user.UserIdentity)
                        .WithOne()
                        .HasForeignKey<AppUser>(user => user.UserId)
                        .IsRequired(true);

            return modelBuilder;
        }
    }
}
