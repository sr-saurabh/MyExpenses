using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Invitation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnInvitationCreation
    {
        public static ModelBuilder OnInvitationCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<UserInvitation>();
            modelBuilder.Entity<UserInvitation>()
                .Property(x => x.InvitationResponse)
                .HasConversion<string>();

            modelBuilder.Entity<UserInvitation>()
                .Property(x => x.InvitationResponse)
                .HasDefaultValue(InvitationResponse.Pending);

            modelBuilder.Entity<UserInvitation>()
                        .Property(x => x.InvitationDate)
                        .IsRequired();

            modelBuilder.Entity<UserInvitation>()
                .Property(x => x.Name)
                .IsRequired();

            return modelBuilder;
        }
    }
}
