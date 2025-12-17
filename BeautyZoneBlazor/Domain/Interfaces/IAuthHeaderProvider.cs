namespace Domain.Interfaces;

public interface IAuthHeaderProvider
{
    Task<string?> GetAuthorizationHeaderAsync(CancellationToken ct = default);
}