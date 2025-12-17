using BeautyZoneBlazor.Auth;
using Microsoft.AspNetCore.Components;

namespace BeautyZoneBlazor.Components.Pages.Auth;

public partial class Logout
{
    [Inject] private AuthStateProvider stateProvider { get; set; } = default!;
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;

    protected async override Task OnInitializedAsync()
    {
        await ((AuthStateProvider)stateProvider).UserLoggedout();
        StateHasChanged();
        _navigationManager.NavigateTo("/login");
    }
}