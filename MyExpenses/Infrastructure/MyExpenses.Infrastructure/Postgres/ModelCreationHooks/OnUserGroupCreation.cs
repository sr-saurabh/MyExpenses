using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnUserGroupCreation
    {
        public static ModelBuilder OnUserGroupCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<UserGroup>();

            return modelBuilder;
        }
    }
}
