using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BeautyZoneBlazor.Components.Pages.BeautyTechs;

public partial class EditBeautyTech
{
    [Parameter]
    public Guid id { get; set; }
    [Inject] private IBeautyTechClient _client { get; set; } = default!;
    [Inject] private NavigationManager _navManager { get; set; } = default!;
    [Inject] private IProcedureClient _procedureClient { get; set; } = default!;
    private BeautyTech _beautyTech = new();
    private List<Procedure> procedures = new();
    private List<SelectBox> selectBoxes = new();
    protected override async Task OnInitializedAsync()
    {
        await GetAsync();
        if (_beautyTech.Procedures != null)
        {
            foreach (var p in _beautyTech.Procedures)
                selectBoxes.Add(new SelectBox() { Id = p.Id });
        }
        selectBoxes.Add(new SelectBox());
    }
    private async Task GetAsync()
    {
        _beautyTech = await _client.GetBeautyTechById(id);
        procedures = await _procedureClient.GetAllProcedures();
    }

    private async Task EditAsync()
    {
        _beautyTech.Procedures = selectBoxes
            .
            Where(s => s.Id.HasValue).
            Select(s => new Procedure {Id = s.Id.Value}).
            ToList();
        var request = new BeautyTechRequest
        {
            Id = _beautyTech.Id,
            Name = _beautyTech.Name,
            PhoneNumber = _beautyTech.PhoneNumber,
            Procedures = _beautyTech.Procedures.Select(p => p.Id).ToList()
        };
        await _client.UpdateBeautyTech(request);
        _navManager.NavigateTo("/beautytechs");
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