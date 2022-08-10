using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetDream.Application.Interfaces;
using PetDream.Application.Mappings;
using PetDream.Application.Services;
using PetDream.Domain.Account;
using PetDream.Domain.Interfaces;
using PetDream.Infra.Data.Context;
using PetDream.Infra.Data.Identity;
using PetDream.Infra.Data.Repositories;

namespace PetDream.Api.Tests
{
    public class BaseTest
    {
        protected IPetOwnerService petOwnerService;
        protected IAuthenticate authenticate;
        protected IMapper mapper;
        protected IPetOwnerRepository petOwnerRepository;
        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly SignInManager<ApplicationUser> signInManager;
        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=localhost;Port=3306;Database=CarlosDiegoApiDb;Uid=root;Pwd=root";

        static BaseTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;
        }

        public BaseTest()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new DomainToDTOMappingProfile());
            });
            mapper = config.CreateMapper();

            var context = new ApplicationDbContext(dbContextOptions);
            
            petOwnerRepository = new PetOwnerRepository(context);
            
            petOwnerService = new PetOwnerService(petOwnerRepository, mapper);
            
            authenticate = new AuthenticateService(userManager, signInManager);
        }
    }
}