using System.Text;
using System.Text.Json;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Clients;

public class ProcedureClient : IProcedureClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public ProcedureClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<List<Procedure>> GetAllProcedures()
    {
        using (var response = await _httpClient.GetAsync("api/Procedure/GetAllProcedures",
                   HttpCompletionOption.ResponseContentRead))
        {
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var procedures = await JsonSerializer.DeserializeAsync<List<Procedure>>(stream, _options);
            return procedures;
        }
    }

    public async Task<Procedure> CreateProcedure(Procedure procedure)
    {
        var json = JsonSerializer.Serialize(procedure, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/Procedure/CreateProcedure", content);
        response.EnsureSuccessStatusCode();
        return await JsonSerializer.DeserializeAsync<Procedure>( response.Content.ReadAsStreamAsync().Result, _options);
    }

    public async Task<Procedure> GetProcedureById(Guid id)
    {
        using (var response = await _httpClient.GetAsync($"api/Procedure/GetProcedureById/{id}",
                   HttpCompletionOption.ResponseContentRead))
        {
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var procedure = await JsonSerializer.DeserializeAsync<Procedure>(stream, _options);
            return procedure;
        }
    }
    

    public async Task<Procedure> UpdateProcedure(Procedure procedure)
    {
        var json = JsonSerializer.Serialize(procedure, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/Procedure/UpdateProcedure/{procedure.Id}", content);
        response.EnsureSuccessStatusCode();
        return await JsonSerializer.DeserializeAsync<Procedure>(response.Content.ReadAsStreamAsync().Result, _options);
    }

    public async Task DeleteProcedure(Procedure procedure)
    {
        var response = await _httpClient.DeleteAsync($"api/Procedure/DeleteProcedure/{procedure.Id}");
        response.EnsureSuccessStatusCode();
    }
}