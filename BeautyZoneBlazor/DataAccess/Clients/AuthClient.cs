using System.Text;
using System.Text.Json;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Clients;

public class AuthClient : IAuthClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public AuthClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        
    }
    public async Task<(bool Success, string Message)> Register(UserRegister user)
    {
        var json = JsonSerializer.Serialize(user, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/api/Account/Register", content);
        if (response.IsSuccessStatusCode)
        {
            return (true, "Registration Successful");
        }
        var error = await response.Content.ReadAsStringAsync();
        return (false, error);
    }

    public async Task<(bool success, string message)> Login(UserLogin login)
    {
        var json = JsonSerializer.Serialize(login, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/api/Account/Login", content);
        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            return (true, token);
        }
        var error = await response.Content.ReadAsStringAsync();
        return (false, error);
    }
}