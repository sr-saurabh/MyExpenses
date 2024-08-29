using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnAuditableEntityCreation
    {
        //create model builder for AuditableEntity
        public static ModelBuilder OnAuditableEntityCreating<T>(this ModelBuilder modelBuilder) where T : AuditableEntity
        {
            modelBuilder.Entity<T>()
                        .HasKey(x => x.Id);
            modelBuilder.Entity<T>()
                        .Property(t => t.Id).IsRequired();

            return modelBuilder;
        }
    }
}
