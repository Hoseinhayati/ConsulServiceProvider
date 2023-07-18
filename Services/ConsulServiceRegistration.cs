using Consul;

namespace ServiceProvider.Services
{
    public static class ConsulServiceRegistration
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration, IHostApplicationLifetime appLifetime)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var serviceName = configuration.GetValue<string>("Service:Name");
            var serviceId = serviceName + "-" + Guid.NewGuid().ToString();
            var serviceHost = configuration.GetValue<string>("Service:Host");
            var servicePort = configuration.GetValue<int>("Service:Port");

            var registration = new AgentServiceRegistration
            {
                ID = serviceId,
                Name = serviceName,
                Address = serviceHost,
                Port = servicePort,
                Tags = new[] { "ASP.NET Core Web API" }
            };

            consulClient.Agent.ServiceRegister(registration).GetAwaiter().GetResult();

            appLifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
            });

            return app;
        }
    }
}
