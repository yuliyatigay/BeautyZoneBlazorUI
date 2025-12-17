using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BeautyZoneBlazor.Components.Pages.Employees;

public partial class CreateEmployee
{
    [Inject] private IEmployeeClient _client { get; set; } = default!;
    [Inject] private NavigationManager _navManager { get; set; } = default!;
    [Inject] private IProcedureClient _procedureClient { get; set; } = default!;
    private Employee employee = new();
    private List<Procedure> procedures = new();
    private List<SelectBox> selectBoxes = new();
    
    protected override async Task OnInitializedAsync()
    {
        procedures = await _procedureClient.GetAllProcedures();
        selectBoxes.Add(new SelectBox());
    }
    private async Task CreateAsync()
    {
        employee.Procedures = selectBoxes
            .Where(s => s.Id.HasValue)
            .Select(s => new Procedure { Id = s.Id!.Value })
            .ToList();
        var request = new EmployeeRequest
        {
            Id = employee.Id,
            Name = employee.Name,
            PhoneNumber = employee.PhoneNumber,
            Procedures = employee.Procedures.Select(p => p.Id).ToList()
        };
        await _client.CreateEmployee(request);
        _navManager.NavigateTo("/employees");
    }
    private async Task GetProceduresAsync()
    {
        procedures = await _procedureClient.GetAllProcedures();
    }
    private void RemoveSelectBox(SelectBox item)
    {
        if (selectBoxes.Count > 1)
            selectBoxes.Remove(item);
    }
    private void AddSelectBox()
    {
        selectBoxes.Add(new SelectBox());
    }
    

}
public class SelectBox
{
    public Guid? Id { get; set; }
}