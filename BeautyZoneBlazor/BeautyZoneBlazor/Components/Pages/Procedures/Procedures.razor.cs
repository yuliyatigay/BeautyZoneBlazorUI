using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BeautyZoneBlazor.Components.Pages.Procedures;

public partial class Procedures
{
    private List<Procedure> procedures = new();
    [Inject] private IProcedureClient _client { get; set; } = default!;
    [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;
    private int counter = 1;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await GetAsync();
        StateHasChanged();
    }

    private async Task GetAsync()
    {
        procedures = await _client.GetAllProcedures();
    }

    private async Task DeleteAsync(Procedure procedure)
    {
        var ok = await _jsRuntime.InvokeAsync<bool>("confirm", "Удалить выбранную процедуру?");
        if (!ok) return;
        await _client.DeleteProcedure(procedure);
        await GetAsync();
        StateHasChanged();
    }
    private int Increment() => counter++;
}