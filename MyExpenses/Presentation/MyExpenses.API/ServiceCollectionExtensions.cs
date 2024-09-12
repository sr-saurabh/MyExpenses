using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyExpenses.API.Services;
using MyExpenses.Application.Abstraction;
using MyExpenses.Application.Providers;
using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Domain.core.MappingProfile;
using MyExpenses.Domain.core.Repositories;
using MyExpenses.Infrastructure.Postgres;
using MyExpenses.Infrastructure.Postgres.Repositories;
using System.Text;

namespace MyExpenses.API
{
    public static class ServiceCollectionExtensions
    {
        public static IConfiguration configuration;

        public static IServiceCollection AddAllDependencies(this IServiceCollection services)
        {
            try
            {
                services.AddHttpContextAccessor();
                services.AddAppDbContext();
                services.AddIdentityServices();
                services.AddContractDependency();
                services.AddRepoDependency();
                services.AddMapperProfile();
                services.AddJwtAuthentication();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddAllDependencies: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }

            return services;
        }

        public static IServiceCollection AddAppDbContext(this IServiceCollection services)
        {
            services.AddDbContext<MyExpensesDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(s =>
            {
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Key"])),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidAudience = configuration["JWT:ValidAudience"],
                     ValidIssuer = configuration["JWT:ValidIssuer"],
                     ClockSkew = TimeSpan.FromMinutes(10) // Increased skew allowance
                 };
             });

            return services;
        }

        public static IServiceCollection AddContractDependency(this IServiceCollection services)
        {
            services.AddScoped<IAuthContract, AuthProvider>()
                    .AddScoped<IGroupContract, GroupProvider>()
                    .AddScoped<IGroupMembershipContract, GroupMembershipProvider>()
                    .AddScoped<IPersonalExpenseContract, PersonalExpenseProvider>()
                    .AddScoped<IContactContract, ContactProvider>()
                    .AddScoped<IGroupExpenseShareContract, ExpenseShareProvider>()
                    .AddScoped<IGroupExpenseContract, GroupExpenseProvider>()
                    .AddScoped<IAppUserContract, AppUserProvider>();
            return services;

        }
        public static IServiceCollection AddRepoDependency(this IServiceCollection services)
        {
            services.AddScoped<IAuthHelperContract, AuthHelper>()
                    //.AddScoped(typeof(IAuditableRepo<>), typeof(AuditableRepo<>))
                    .AddScoped<IAppUserRepo, AppUserRepo>()
                    .AddScoped<IGroupMembershipRepo, GroupMembershipRepo>()
                    .AddScoped<IPersonalExpenseRepo, PersonalExpenseRepo>()
                    .AddScoped<IContactRepo, ContactRepo>()
                    .AddScoped<IGroupExpenseRepo, GroupExpenseRepo>()
                    .AddScoped<IGroupExpenseShareRepo, GroupExpenseShareRepo>()
                    .AddScoped<IGroupRepo, GroupRepo>();
            ;
            return services;

        }


        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services
               .AddIdentity<AppIdentityUser, AppIdentityRole>()
               .AddEntityFrameworkStores<MyExpensesDbContext>()
               .AddDefaultTokenProviders();

            return services;
        }


        public static IServiceCollection AddMapperProfile(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppUserMapperProfile).Assembly);
            return services;
        }
    }
}
