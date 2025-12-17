using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BeautyZoneBlazor.Components.Pages.Procedures;

public partial class EditProcedure
{
    [Parameter]
    public Guid id { get; set; }
    [Inject] private IProcedureClient _client { get; set; } = default!;
    [Inject] private NavigationManager _navManager { get; set; } = default!;
    private Procedure procedure = new();
    private async Task EditAsync()
    {
        await _client.UpdateProcedure(procedure);
        _navManager.NavigateTo("/procedures");
    }
}