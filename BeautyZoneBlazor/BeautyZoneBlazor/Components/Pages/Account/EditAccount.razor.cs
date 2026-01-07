using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BeautyZoneBlazor.Components.Pages.Account;

public partial class EditAccount
{
    [Parameter] public Guid id { get; set; }
    [Inject] private IUserClient _client { get; set; } = default!;
    private User user = new();
    private int userRoleValue
    {
        get => (int)user.Role;
        set => user.Role = (UserRole)value;
    }
    private string? errorMessage { get; set; } = "";
    private string? successMessage { get; set; } = "";
    protected override async Task OnInitializedAsync()
    {
        user = await _client.GetUserById(id);
    }

    private async Task EditAsync()
    {
        await _client.UpdateAccount(user);
    }
}