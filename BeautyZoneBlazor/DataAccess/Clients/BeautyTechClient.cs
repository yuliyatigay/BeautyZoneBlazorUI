using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Clients;

public class BeautyTechClient : IBeautyTechClient
{
    private readonly IAuthHttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public BeautyTechClient(IAuthHttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<List<BeautyTech>> GetAllBeautyTechs()
    {
         var request = new HttpRequestMessage(
                HttpMethod.Get, "api/BeautyTech/FetchAllBeautyTechs");
        var response = await _httpClient.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return new List<BeautyTech>();
        }
        return await response.Content
        .ReadFromJsonAsync<List<BeautyTech>>(_options) ?? [];
        
        
    }

    public async Task<BeautyTech> AddBeautyTech(BeautyTechRequest master)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/BeautyTech/AddBeautyTech")
        {
            Content = JsonContent.Create(master, options: _options)
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BeautyTech>(_options);
    }

    public async Task<BeautyTech> GetBeautyTechById(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/BeautyTech/GetBeautyTechById/{id}");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BeautyTech>(_options);
    }

    public Task<List<BeautyTech>> GetBeautyTechsByProcedureName(string procedureName)
    {
        throw new NotImplementedException();
    }

    public async Task<BeautyTech> UpdateBeautyTech(BeautyTechRequest beautyTech)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/BeautyTech/UpdateBeautyTechAsync/{beautyTech.Id}")
        {
            Content = JsonContent.Create(beautyTech, options: _options)
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BeautyTech>(_options);
    }

    public async Task DeleteBeautyTech(BeautyTech beautyTech)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Delete, $"api/BeautyTech/DeleteBeautyTechAsync/{beautyTech.Id}");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}