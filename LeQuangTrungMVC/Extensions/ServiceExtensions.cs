using LeQuangTrungMVC.BusinessLogicLayer.Implementations;
using LeQuangTrungMVC.BusinessLogicLayer.Interfaces;
using LeQuangTrungMVC.BusinessObjects.Configurations;
using LeQuangTrungMVC.Repository.Implementations;
using LeQuangTrungMVC.Repository.Interfaces;

namespace LeQuangTrungMVC.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<INewsArticleRepository, NewsArticleRepsitory>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<INewsArticleService, NewsArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
        public static void AddAdminAccountConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<AdminAccount>(configuration.GetSection("AdminAccount"));
    }
}
