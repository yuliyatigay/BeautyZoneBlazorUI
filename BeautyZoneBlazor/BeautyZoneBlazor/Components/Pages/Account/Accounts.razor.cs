using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BeautyZoneBlazor.Components.Pages.Account;

public partial class Accounts
{
    [Inject] private IUserClient _client { get; set; } = default!;
    [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;
    private List<User> users = new();
    private int counter = 1;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await GetAsync();
        StateHasChanged();
    }

    private async Task GetAsync()
    {
        users = await _client.GetAllUsers();
    }

    private async Task DeleteAsync(User user)
    {
        var ok = await _jsRuntime.InvokeAsync<bool>("confirm", "Удалить выбранный аккаунт?");
        if (!ok) return;
        await _client.DeleteUser(user.Id);
        await GetAsync();
        StateHasChanged();
    }
    private int Increment() => counter++;
}