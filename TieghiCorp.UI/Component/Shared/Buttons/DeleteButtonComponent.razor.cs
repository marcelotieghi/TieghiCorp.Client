using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Component.Shared.Dialog;
using TieghiCorp.UI.Core.Api;
using TieghiCorp.UI.Core.Models.Abstract;
using TieghiCorp.UI.Core.Services;

namespace TieghiCorp.UI.Component.Shared.Buttons;

public partial class DeleteButtonComponent<TModel> : EditButtonComponent<TModel> where TModel : BaseModel
{
    protected string Icon = Icons.Material.Filled.Delete;

    #region Paramenters

    [Parameter]
    public EventCallback OnDeleteSuccess { get; set; }

    #endregion

    #region Services

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private IServices<TModel> Services { get; set; } = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    #endregion

    #region Methods

    private async Task DeleteItem(TModel? model)
    {
        try
        {
            if (model is null) return;

            var url = ApiRoutes.GetUrl<TModel>();
            var response = await Services.DeleteAsync(url, model.Id);

            if (response.Success)
            {
                Snackbar.Add($"{typeof(TModel).Name} deleted successfully.", Severity.Success);
                await OnDeleteSuccess.InvokeAsync();
            }
            else
                Snackbar.Add($"Failed to delete {typeof(TModel).Name}: {response.Message}", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
        }
    }

    protected async Task OpenDialogAsync(TModel model)
    {
        try
        {
            Icon = Icons.Material.Outlined.Delete;

            var parameters = new DialogParameters<DialogConfirmationComponent>
                {
                    { x => x.ContentText, $"Do you really want to delete the '{typeof(TModel).Name}: {model.BaseName}'?" },
                    { x => x.Title, "Confirm Delete"},
                    { x => x.ButtonText, "Delete" },
                    { x => x.Color, Color.Error },
                    { x => x.OnConfirm, EventCallback.Factory.Create(this, () => DeleteItem(model)) },
                    { x => x.OnCancel, EventCallback.Factory.Create(this, () => Icon = Icons.Material.Filled.Delete) }
                };

            var options = new DialogOptions { CloseButton = false, MaxWidth = MaxWidth.Small };

            await DialogService.ShowAsync<DialogConfirmationComponent>("Confirm Deletion", parameters, options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion
}