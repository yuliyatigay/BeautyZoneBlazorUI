using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BeautyZoneBlazor.Components.Pages.Customers;

public partial class Customers
{
    [Inject] private ICustomerClient _customerClient { get; set; } = default!;
    [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;
    private List<Customer> customers = new();
    private int counter = 1;
    protected override async Task OnInitializedAsync()
    {
        customers = await _customerClient.GetAllCustomers();
    }
    private async Task DeleteAsync(Customer customer)
    {
        var ok = await _jsRuntime.InvokeAsync<bool>("confirm", "Удалить клиента?");
        if (!ok) return;
        await _customerClient.DeleteCustomer(customer.Id);
        customers.Remove(customer);
        StateHasChanged();
    }
    private int Increment() => counter++;
}