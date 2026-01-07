using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BeautyZoneBlazor.Components.Pages.BeautyTechs;

public partial class AddBeautyTech
{
    [Inject] private IBeautyTechClient _client { get; set; } = default!;
    [Inject] private NavigationManager _navManager { get; set; } = default!;
    [Inject] private IProcedureClient _procedureClient { get; set; } = default!;
    private BeautyTech _beautyTech = new();
    private List<Procedure> procedures = new();
    private List<SelectBox> selectBoxes = new();
    
    protected override async Task OnInitializedAsync()
    {
        procedures = await _procedureClient.GetAllProcedures();
        selectBoxes.Add(new SelectBox());
    }
    private async Task CreateAsync()
    {
        _beautyTech.Procedures = selectBoxes
            .Where(s => s.Id.HasValue)
            .Select(s => new Procedure { Id = s.Id!.Value })
            .ToList();
        var request = new BeautyTechRequest
        {
            Id = _beautyTech.Id,
            Name = _beautyTech.Name,
            PhoneNumber = _beautyTech.PhoneNumber,
            Procedures = _beautyTech.Procedures.Select(p => p.Id).ToList()
        };
        await _client.AddBeautyTech(request);
        _navManager.NavigateTo("/beautytechs");
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