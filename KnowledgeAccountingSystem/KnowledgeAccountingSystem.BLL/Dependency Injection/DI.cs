using KnowledgeAccountingSystem.DAL;
using KnowledgeAccountingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IProgrammerService, ProgrammerService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IStatService, StatisticService>();
        }
    }
}
