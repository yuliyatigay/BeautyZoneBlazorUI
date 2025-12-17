namespace Domain.Interfaces;

public interface IAuthHttpClient
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken ct = default);
}