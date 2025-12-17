using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BeautyZoneBlazor.Components.Pages.Employees;

public partial class Employees
{
    [Inject] private IEmployeeClient _client { get; set; } = default!;
    [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;
    private List<Employee> employees = new();
    private int counter = 1;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await GetAsync();
        StateHasChanged();
    }
    private async Task GetAsync()
    {
        employees = await _client.GetAllEmployees();
    }
    private async Task DeleteAsync(Employee employee)
    {
        var ok = await _jsRuntime.InvokeAsync<bool>("confirm", "Удалить мастера?");
        if (!ok) return;
        await _client.DeleteEmployee(employee);
        await GetAsync();
        StateHasChanged();
    }
    private int Increment() => counter++;
}