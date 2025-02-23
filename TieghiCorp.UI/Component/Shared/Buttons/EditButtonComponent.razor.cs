using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Core.Api;
using TieghiCorp.UI.Core.Models.Abstract;
using TieghiCorp.UI.Core.Services;

namespace TieghiCorp.UI.Component.Shared.Buttons;

public partial class EditButtonComponent<TModel> : ComponentBase where TModel : BaseModel
{
    #region Properties

    private TModel? ItemBeforeEdit { get; set; }

    #endregion

    #region Parameters

    [Parameter]
    public required EditButtonContext Button { get; set; }

    #endregion

    #region Services

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private IServices<TModel> Services { get; set; } = null!;

    #endregion

    #region Methods

    public void BackupItem(object? item)
    {
        if (item is not TModel model) return;

        ItemBeforeEdit = Activator.CreateInstance<TModel>();
        foreach (var property in typeof(TModel).GetProperties())
        {
            if (property.GetSetMethod() is null || property.GetSetMethod() is { IsAbstract: true })
                continue;

            property.SetValue(ItemBeforeEdit, property.GetValue(model));
        }

        StateHasChanged();
    }

    public void ResetItemToOriginalValues(object? item)
    {
        if (item is not TModel model || ItemBeforeEdit == null) return;

        foreach (var property in typeof(TModel).GetProperties())
        {
            if (property.GetSetMethod() is null || property.GetSetMethod() is { IsAbstract: true })
                continue;

            property.SetValue(model, property.GetValue(ItemBeforeEdit));
        }

        StateHasChanged();
    }

    public async void ItemHasBeenCommitted(object? item)
    {
        try
        {
            StateHasChanged();

            if (item is not TModel model) return;

            var url = ApiRoutes.GetUrl<TModel>();
            var result = await Services.UpdateAsync(url, model);

            if (result.Success)
                Snackbar.Add($"{typeof(TModel).Name} updated successfully", Severity.Success);
            else
                Snackbar.Add($"Failed to update {typeof(TModel).Name}", Severity.Error);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Snackbar.Add($"An unexpected error occurred while updating the {typeof(TModel).Name}", Severity.Error);
        }
    }

    #endregion
}