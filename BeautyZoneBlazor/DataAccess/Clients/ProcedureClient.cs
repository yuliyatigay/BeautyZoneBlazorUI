using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Clients;

public class ProcedureClient : IProcedureClient
{
    private readonly IAuthHttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public ProcedureClient(IAuthHttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<List<Procedure>> GetAllProcedures()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/Procedure/GetAllProcedures");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Procedure>>(_options);
    }

    public async Task<Procedure> CreateProcedure(Procedure procedure)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/Procedure/CreateProcedure")
        {
            Content = JsonContent.Create(procedure, options: _options)
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Procedure>(_options);
    }

    public async Task<Procedure> GetProcedureById(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/Procedure/GetProcedureById/{id}");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Procedure>(_options);
    }
    

    public async Task<Procedure> UpdateProcedure(Procedure procedure)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/Procedure/UpdateProcedure/{procedure.Id}")
        {
            Content = JsonContent.Create(procedure, options: _options)
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Procedure>(_options);
    }

    public async Task DeleteProcedure(Procedure procedure)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Procedure/DeleteProcedure/{procedure.Id}");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}