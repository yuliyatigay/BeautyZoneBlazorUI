using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BeautyZoneBlazor.Components.Pages.Auth;

public partial class Registration
{
    [Inject] private IAuthClient _client { get; set; } = default!;
    private UserRegister user = new();
    private string? errorMessage { get; set; } = "";
    private string? successMessage { get; set; } = "";

    private async Task RegisterAsync()
    {
        var (success, result) = await _client.Register(user);
        if (!success)
        {
            errorMessage = result;
        }
        else
        {
            successMessage = result;
        }
    }
}