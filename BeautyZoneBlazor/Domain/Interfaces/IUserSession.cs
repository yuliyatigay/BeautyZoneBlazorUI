namespace Domain.Interfaces;

public interface IUserSession
{
    string? Token { get; }
    void SetToken(string token);
    void Clear();
}