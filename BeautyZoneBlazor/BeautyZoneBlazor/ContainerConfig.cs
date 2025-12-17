using System.Text.Json;
using DataAccess.Clients;
using Domain.Interfaces;

namespace BeautyZoneBlazor;

public static class ContainerConfig
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection("ApiSettings");
        services.AddHttpClient<IProcedureClient, ProcedureClient>(client =>
        {
             client.BaseAddress = new Uri(apiSettings.GetSection("BaseAddress").Value);
        });
        services.AddHttpClient<IEmployeeClient, EmployeeClient>(client =>
        {
            client.BaseAddress = new Uri(apiSettings.GetSection("BaseAddress").Value);
        });
        services.AddHttpClient<ICustomerClient, CustomerClient>(client =>
        {
            client.BaseAddress = new Uri(apiSettings.GetSection("BaseAddress").Value);
        });
    }
}