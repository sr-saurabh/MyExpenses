using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Relationships;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnContactCreation
    {
        public static ModelBuilder OnContactCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<Contact>();

            modelBuilder.Entity<Contact>()
                        .HasOne(c => c.FromUser)
                        .WithMany(u => u.FromContacts)
                        .HasForeignKey(c => c.FromUserId);            

            modelBuilder.Entity<Contact>()
                        .Property(Contact => Contact.Status)
                        .HasDefaultValue(ContactInvitationStatus.Pending);

            modelBuilder.Entity<Contact>()
                        .HasOne(c => c.ToUser)
                        .WithMany(u => u.ToContacts)
                        .HasForeignKey(c => c.ToUserId);


            return modelBuilder;
        }

    }
}
