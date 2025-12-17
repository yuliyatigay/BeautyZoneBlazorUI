using System.Text.Json;
using BeautyZoneBlazor.Auth;
using DataAccess.Clients;
using Domain.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BeautyZoneBlazor;

public static class ContainerConfig
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection("ApiSettings");
        services.AddScoped<IUserSession, UserSession>();
        services.AddScoped<AuthStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(sp => 
            sp.GetRequiredService<AuthStateProvider>());
        
        services.AddCascadingAuthenticationState();
        services.AddAuthorizationCore();

        services.AddHttpClient<IAuthClient, AuthClient>(client =>
        {
            client.BaseAddress = new Uri(apiSettings.GetSection("BaseAddress").Value);
        });

        services.AddHttpClient<AuthHttpClient>(client =>
        {
            client.BaseAddress = new Uri(apiSettings.GetSection("BaseAddress").Value);
        });
        services.AddScoped<IAuthHttpClient>(sp =>
            sp.GetRequiredService<AuthHttpClient>());

        services.AddScoped<ICustomerClient, CustomerClient>();
        services.AddScoped<IEmployeeClient, EmployeeClient>();
        services.AddScoped<IProcedureClient, ProcedureClient>();
        
    }
}