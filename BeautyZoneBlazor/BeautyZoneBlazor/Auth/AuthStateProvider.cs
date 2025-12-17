using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BeautyZoneBlazor.Auth;

public class AuthStateProvider(ProtectedSessionStorage sessionStorage) : 
    AuthenticationStateProvider
{
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = (await sessionStorage.GetAsync<string>("authToken")).Value;
        var identity = string.IsNullOrEmpty(token)
            ? new ClaimsIdentity() : GetClaimsIdentity(token);
        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    private ClaimsIdentity GetClaimsIdentity(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims;
        return new ClaimsIdentity(claims, "Token");
    }
    private async Task UserAuthenticated(string token)
    {
        await sessionStorage.SetAsync("authToken", token);
        var identity = GetClaimsIdentity(token);
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private async Task UserLoggedOut()
    {
        await sessionStorage.DeleteAsync("authToken");
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}