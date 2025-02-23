using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Core.Models.Abstract;
using TieghiCorp.UI.Core.Services;

namespace TieghiCorp.UI.Component.Shared.Field;

public partial class AutoCompleteComponent<TModel> : ComponentBase where TModel : BaseModel
{
    #region Paramenters

    [Parameter]
    public TModel? SelectedItem { get; set; }

    [Parameter]
    public EventCallback<TModel?> SelectedItemChanged { get; set; }

    [Parameter]
    [Category("Appearance")]
    public Variant Variant { get; set; }

    [Parameter]
    [Category("Behavior")]
    public string? Label { get; set; }

    [Parameter][Category("Validation")] public object? Validation { get; set; }

    #endregion

    #region Properties

    private List<TModel> InitialLocations { get; set; } = [];
    private bool IsBusy { get; set; }

    #endregion

    #region Services

    [Inject]
    private IServices<TModel> Services { get; set; } = null!;

    #endregion

    #region Methods

    protected async Task OnSelectedItemChanged(TModel? value)
    {
        if (SelectedItem == value)
            return;

        SelectedItem = value;
        await SelectedItemChanged.InvokeAsync(value);
    }

    protected async Task<IEnumerable<TModel>> Search(string value, CancellationToken token)
    {
        if (IsBusy) return [];

        try
        {
            IsBusy = true;

            if (string.IsNullOrWhiteSpace(value))
            {
                if (InitialLocations.Count != 0)
                    return InitialLocations;

                var result = await Services.ListAsync("v1/locations", 1, 25, cancellationToken: token);
                InitialLocations = result.Data.ToList();

                return InitialLocations;
            }

            var locations = await Services.ListAsync("v1/locations", 1, 25, searchTerm: value, cancellationToken: token);
            return locations.Data;
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}