using Application;
using Domain.Repositories.Accounts;
using Domain.Repositories.Categories;
using Domain.Repositories.Transactions;
using Domain.Repositories.UserProfiles;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Repository.Accounts;
using Infrastructure.EntityFramework.Repository.Categories;
using Infrastructure.EntityFramework.Repository.Transactions;
using Infrastructure.EntityFramework.Repository.UserProfiles;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharedKernel.Core;
using System.Reflection;
using System.Text;

namespace Infrastructure
{
    public static class Extensions
    {
        public static void AddSecurity(IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
            }).AddEntityFrameworkStores<ReadDbContext>()
            .AddDefaultTokenProviders();
            services.AddAuthorization(config =>
            {
                var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                var defaultAuthPolicy = defaultAuthBuilder
                    .RequireAuthenticatedUser()
                    .Build();

                config.DefaultPolicy = defaultAuthPolicy;

                foreach (var mnemonic in ApplicationPermission.GetAllPermissions().Select(x => x.Mnemonic))
                {
                    config.AddPolicy(mnemonic,
                        policy => policy.RequireClaim("Permission", new string[] { mnemonic }));
                }
            });

            JwtOptions jwtoptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

            services.AddAuthentication().AddJwtBearer("Bearer", jwtOptions =>
            {
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecretKey));
                jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = jwtoptions.ValidateIssuer,
                    ValidateAudience = jwtoptions.ValidateAudience,
                    ValidIssuer = jwtoptions.ValidIssuer,
                    ValidAudience = jwtoptions.ValidAudience
                };
            });
            System.Diagnostics.Debug.WriteLine(jwtoptions == null ? true : false);
            services.AddSingleton(jwtoptions!);


            services.AddScoped<SecurityInitializer>();
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAplication();

            var connectionString = configuration.GetConnectionString("DatabaseConection");

            services.AddDbContext<ReadDbContext>(context => { context.UseSqlServer(connectionString); });
            services.AddDbContext<WriteDbContext>(context => { context.UseSqlServer(connectionString); });

            services.AddHostedService<DbInitializer>();

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            AddSecurity(services, configuration);

            return services;
        }
    }
}

