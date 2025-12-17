using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BeautyZoneBlazor.Components.Pages.Procedures;

public partial class CreateProcedure
{
    [Inject] private IProcedureClient _client { get; set; } = default!;
    [Inject] private NavigationManager _navManager { get; set; } = default!;
    private Procedure procedure = new();

    private async Task CreateAsync()
    {
        await _client.CreateProcedure(procedure);
        _navManager.NavigateTo("/procedures");
    }
}