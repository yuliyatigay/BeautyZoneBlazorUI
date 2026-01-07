using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Clients;

public class UserClient : IUserClient
{
    private readonly IAuthHttpClient _httpClient;
    private readonly JsonSerializerOptions _options;
    public UserClient(IAuthHttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<List<User>> GetAllUsers()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/Account/GetAllAccounts");
        var response = await _httpClient.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return new List<User>();
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<User>>(_options);
    }

    public async Task<User> GetUserById(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/Account/GetAccountById/{id}");
        var response = await _httpClient.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>(_options);
    }

    public async Task<(bool Success, string Message)> UpdateAccount(User user)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Put,
            $"api/Account/UpdateAccount/{user.Id}")
        {
            Content = JsonContent.Create(user, options: _options)
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            return (true, "Обновление успешно!");
        }
        var error = await response.Content.ReadAsStringAsync();
        return (false, error);
    }

    public async Task DeleteUser(Guid userId)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Account/DeleteAccount/{userId}");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}