using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Clients;

public class EmployeeClient : IEmployeeClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public EmployeeClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<List<Employee>> GetAllEmployees()
    {
        using (var response = await _httpClient.GetAsync("api/Employee/GetAllEmployees", 
                   HttpCompletionOption.ResponseContentRead))
        {
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var employees = await JsonSerializer.DeserializeAsync<List<Employee>>(stream, _options);
            return employees;
        }
    }

    public async Task<Employee> CreateEmployee(EmployeeRequest master)
    {
        var json = JsonSerializer.Serialize(master, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/Employee/AddEmployee", content);
        response.EnsureSuccessStatusCode();
        return await JsonSerializer.DeserializeAsync<Employee>(response.Content.ReadAsStreamAsync().Result, _options);
    }

    public async Task<Employee> GetEmployeeById(Guid id)
    {
        using (var response = await _httpClient.GetAsync($"api/Employee/GetEmployeeById/{id}", 
                   HttpCompletionOption.ResponseContentRead))
        {
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var employee = await JsonSerializer.DeserializeAsync<Employee>(stream, _options);
            return employee;
        }
    }

    public Task<List<Employee>> GetEmployeesByProcedure(string procedureName)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> UpdateEmployee(EmployeeRequest master)
    {
        var json = JsonSerializer.Serialize(master, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/Employee/UpdateEmployee/{master.Id}", content);
        response.EnsureSuccessStatusCode();
        return await JsonSerializer.DeserializeAsync<Employee>(response.Content.ReadAsStreamAsync().Result, _options);
    }

    public Task DeleteEmployee(Employee master)
    {
        var json = JsonSerializer.Serialize(master, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return _httpClient.DeleteAsync($"api/Employee/DeleteEmployee/{master.Id}");
    }
}