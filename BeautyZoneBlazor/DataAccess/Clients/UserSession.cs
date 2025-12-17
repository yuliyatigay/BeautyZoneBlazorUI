using Domain.Interfaces;

namespace DataAccess.Clients;

public class UserSession : IUserSession
{
    public string? Token { get; private set; }
    public void SetToken(string token) => Token = token;
    public void Clear() => Token = null;
}