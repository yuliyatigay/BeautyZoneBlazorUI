using Domain.Interfaces;

namespace DataAccess.Clients;

public sealed class AuthHttpClient : IAuthHttpClient
{
    private readonly HttpClient _http;
    private readonly IUserSession _session;

    public AuthHttpClient(HttpClient http, IUserSession session)
    {
        _http = http;
        _session = session;
    }

    public Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(_session.Token))
        {
            request.Headers.Remove("Authorization");
            request.Headers.Add("Authorization", $"Bearer {_session.Token}");
        }

        return _http.SendAsync(request, ct);
    }
}