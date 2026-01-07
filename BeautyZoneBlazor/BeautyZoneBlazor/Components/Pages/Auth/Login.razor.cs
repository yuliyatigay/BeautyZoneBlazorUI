using BeautyZoneBlazor.Auth;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BeautyZoneBlazor.Components.Pages.Auth;

public partial class Login
{
    [Inject] public IAuthClient _authClient { get; set; } = default!;
    [Inject] public NavigationManager _navigation { get; set; } = default!;
    [Inject] public AuthStateProvider _stateProvider { get; set; } = default!;
    [Inject] private IUserSession _session { get; set; } = default!;
    [Inject] private ProtectedLocalStorage _storage { get; set; } = default!;
    public UserLogin user = new UserLogin();
    public string ErrorMessage { get; set; } = "";

    private async Task LoginAsync()
    {
        var (success, result) = await _authClient.Login(user);
        
        if (!success)
        {
            ErrorMessage = result;
            return;
        }
        _session.SetToken(result);
        await _stateProvider.UserAuthentificated(result);
        _navigation.NavigateTo("/");
    }
}