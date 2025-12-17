using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Clients;

public class CustomerClient : ICustomerClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public CustomerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<List<Customer>> GetAllCustomers()
    {
        using (var response = await _httpClient.GetAsync("api/Customer/GetAllCustomers", 
                   HttpCompletionOption.ResponseContentRead))
        {
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var customers = await JsonSerializer.DeserializeAsync<List<Customer>>(stream, _options);
            return customers;
        }
    }

    public async Task<Customer> GetCustomerById(Guid id)
    {
        using (var response = await _httpClient.GetAsync($"api/Customer/GetCustomerById/{id}"))
        {
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var customer = await JsonSerializer.DeserializeAsync<Customer>(stream, _options);
            return customer;
        }
    }

    public async Task CreateCustomer(Customer customer)
    {
        var json = JsonSerializer.Serialize(customer, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/Customer/CreateCustomer", content);
        response.EnsureSuccessStatusCode();
        await JsonSerializer.DeserializeAsync<Customer>(response.Content.ReadAsStreamAsync().Result, _options);
    }

    public async Task UpdateCustomer(Customer customer)
    {
        var json = JsonSerializer.Serialize(customer, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/Customer/UpdateCustomer/{customer.Id}", content);
        response.EnsureSuccessStatusCode();
        await JsonSerializer.DeserializeAsync<Customer>(response.Content.ReadAsStreamAsync().Result, _options);
    }

    public Task DeleteCustomer(Customer customer)
    {
        var json = JsonSerializer.Serialize(customer, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return _httpClient.DeleteAsync($"api/Customer/DeleteCustomer/{customer.Id}");
    }
}