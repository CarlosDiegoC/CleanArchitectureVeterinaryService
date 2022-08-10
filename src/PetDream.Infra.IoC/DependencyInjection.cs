using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetDream.Application.Interfaces;
using PetDream.Application.Mappings;
using PetDream.Application.Services;
using PetDream.Domain.Account;
using PetDream.Domain.Interfaces;
using PetDream.Infra.Data.Context;
using PetDream.Infra.Data.Identity;
using PetDream.Infra.Data.Repositories;

namespace PetDream.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection"); 
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddScoped<IPetOwnerRepository, PetOwnerRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IVeterinarianRepository, VeterinarianRepository>();
            services.AddScoped<IVeterinaryCareRecordRepository, VeterinaryCareRecordRepository>();
            services.AddScoped<IPetOwnerService, PetOwnerService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IVeterinarianService, VeterinarianService>();
            services.AddScoped<IVeterinaryCareRecordService, VeterinaryCareRecordService>();
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            return services;
        }
    }
}