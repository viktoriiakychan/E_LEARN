using _18_E_LEARN.DataAccess.Data.IRepository;
using _18_E_LEARN.DataAccess.Data.Repository;

namespace _18_E_LEARN.Web.Infrastructure.Repository
{
    public class RepositoryConfiguration
    {
        public static void Config(IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>(); // connect
        }
    }
}
