using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Core.Api;
using TieghiCorp.UI.Core.Models.Abstract;
using TieghiCorp.UI.Core.Services;

namespace TieghiCorp.UI.Component.Shared.Field;

public partial class SelectComponent<TModel> : ComponentBase where TModel : BaseModel
{
    #region Parameters

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

    [Parameter]
    [Category("Validation")]
    public object? Validation { get; set; }

    #endregion

    #region Properties

    private List<TModel> Items { get; set; } = [];
    private bool IsBusy { get; set; }

    #endregion

    #region Services

    [Inject]
    private IServices<TModel> Services { get; set; } = null!;

    #endregion

    #region Methods

    protected override async Task OnInitializedAsync()
    {
        await LoadItems();
    }

    private async Task LoadItems()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var result = await Services.ListAsync(ApiRoutes.GetUrl<TModel>(), 1, 25);
            Items = result.Data.ToList();
        }
        finally
        {
            IsBusy = false;
        }
    }

    protected async Task OnSelectedItemChanged(TModel? value)
    {
        if (SelectedItem == value)
            return;

        SelectedItem = value;
        await SelectedItemChanged.InvokeAsync(value);
    }

    #endregion

    [Parameter]
    public string DisplayProperty { get; set; } = "ToString";

    private string GetDisplayValue(TModel item)
    {
        var prop = typeof(TModel).GetProperty(DisplayProperty);
        return prop?.GetValue(item)?.ToString() ?? string.Empty;
    }

}