using Consul;

namespace ServiceProvider.Configuration
{
    public static class ConsulConfig
    {
        public static void ConfigureConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri("http://localhost:8500");
            }));
        }
    }
}
