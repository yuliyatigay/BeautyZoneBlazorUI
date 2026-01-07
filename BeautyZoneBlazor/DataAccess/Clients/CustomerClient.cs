using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Clients;

public class CustomerClient : ICustomerClient
{
    private readonly IAuthHttpClient _http;
    private readonly JsonSerializerOptions _options;

    public CustomerClient(IAuthHttpClient http)
    {
        _http = http;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/Customer/GetAllCustomers");

        var response = await _http.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return new List<Customer>();
        }
        return await response.Content
            .ReadFromJsonAsync<List<Customer>>(_options)
            ?? [];
    }

    public async Task<Customer?> GetCustomerById(Guid id)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"/api/Customer/GetCustomerById/{id}");

        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<Customer>(_options);
    }

    public async Task CreateCustomer(Customer customer)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Post,
            "/api/Customer/CreateCustomer")
        {
            Content = JsonContent.Create(customer, options: _options)
        };

        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateCustomer(Customer customer)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Put,
            $"/api/Customer/UpdateCustomer/{customer.Id}")
        {
            Content = JsonContent.Create(customer, options: _options)
        };

        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCustomer(Guid customerId)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"/api/Customer/DeleteCustomer/{customerId}");

        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}