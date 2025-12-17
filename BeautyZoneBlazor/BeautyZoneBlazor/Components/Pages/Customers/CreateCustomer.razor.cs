using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BeautyZoneBlazor.Components.Pages.Customers;

public partial class CreateCustomer
{
    [Inject] private ICustomerClient _customerClient { get; set; } = default!;
    [Inject] private NavigationManager _navManager { get; set; } = default!;
    private Customer customer = new();

    private async Task CreateAsync()
    {
        await _customerClient.CreateCustomer(customer);
        _navManager.NavigateTo("/customers");
    }
}