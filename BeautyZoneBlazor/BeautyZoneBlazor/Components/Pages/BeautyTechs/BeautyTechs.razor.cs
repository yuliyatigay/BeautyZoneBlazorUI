using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BeautyZoneBlazor.Components.Pages.BeautyTechs;

public partial class BeautyTechs
{
    [Inject] private IBeautyTechClient _client { get; set; } = default!;
    [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;
    private List<BeautyTech> beautyTechs = new();
    private int counter = 1;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await GetAsync();
        StateHasChanged();
    }
    private async Task GetAsync()
    {
        beautyTechs = await _client.GetAllBeautyTechs();
    }
    private async Task DeleteAsync(BeautyTech beautyTech)
    {
        var ok = await _jsRuntime.InvokeAsync<bool>("confirm", "Удалить мастера?");
        if (!ok) return;
        await _client.DeleteBeautyTech(beautyTech);
        await GetAsync();
        StateHasChanged();
    }
    private int Increment() => counter++;
}