using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BeautyZoneBlazor.Components.Pages.Customers;

public partial class EditCustomer
{
    [Inject] private ICustomerClient _customerClient { get; set; } = default!;
    [Parameter] public Guid id { get; set; }
    private Customer customer = new();

    protected override async Task OnInitializedAsync()
    {
        customer = await _customerClient.GetCustomerById(id);
    }

    private async Task EditAsync()
    {
        await _customerClient.UpdateCustomer(customer);
    }
}