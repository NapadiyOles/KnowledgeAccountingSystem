using KnowledgeAccountingSystem.DAL;
using KnowledgeAccountingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using KnowledgeAccountingSystem.BLL.Mapper;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Services;

namespace KnowledgeAccountingSystem.BLL.Dependency_Injection
{
    public static class DI
    {
        public static void AddDependencyDAL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<KnowledgeDbContext>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddDependencyBLL(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new AutoMapper.Mapper(new MapperConfiguration(config =>
            config.AddProfile<AutomapperProfile>())));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminServise, AdminService>();
        }
    }
}
