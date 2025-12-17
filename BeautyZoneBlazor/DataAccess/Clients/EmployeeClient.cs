using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Clients;

public class EmployeeClient : IEmployeeClient
{
    private readonly IAuthHttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public EmployeeClient(IAuthHttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<List<Employee>> GetAllEmployees()
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get, "api/Employee/GetAllEmployees");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content
            .ReadFromJsonAsync<List<Employee>>(_options) ?? [];
    }

    public async Task<Employee> CreateEmployee(EmployeeRequest master)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/Employee/AddEmployee")
        {
            Content = JsonContent.Create(master, options: _options)
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Employee>(_options);
    }

    public async Task<Employee> GetEmployeeById(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/Employee/GetEmployeeById/{id}");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Employee>(_options);
    }

    public Task<List<Employee>> GetEmployeesByProcedure(string procedureName)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> UpdateEmployee(EmployeeRequest master)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/Employee/UpdateEmployee/{master.Id}")
        {
            Content = JsonContent.Create(master, options: _options)
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Employee>(_options);
    }

    public async Task DeleteEmployee(Employee master)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Delete, $"api/Employee/DeleteEmployee/{master.Id}");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}