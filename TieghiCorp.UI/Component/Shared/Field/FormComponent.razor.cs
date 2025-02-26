using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Core.Api;
using TieghiCorp.UI.Core.Models.Abstract;
using TieghiCorp.UI.Core.Services;

namespace TieghiCorp.UI.Component.Shared.Field;

public partial class FormComponent<TModel> where TModel : BaseModel
{
    #region Paramenters

    [Parameter] public required RenderFragment Inputs { get; set; }
    [Parameter] public object? Model { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;

    #endregion

    #region Properties

    private bool IsBusy { get; set; }
    private MudForm? EditForm { get; set; }

    #endregion

    #region Services

    [Inject]
    public IServices<TModel> Handler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    #endregion

    #region Methods

    private async Task HandleSave()
    {
        IsBusy = true;

        try
        {
            var url = ApiRoutes.GetUrl<TModel>();
            var result = await Handler.CreateAsync(url, (TModel)Model!);

            if (result.Success)
            {
                Snackbar.Add($"{typeof(TModel).Name} created successfully", Severity.Success);
                NavigationManager.NavigateTo($"{typeof(TModel).Name}");
            }
            else
            {
                var errorMessage = !string.IsNullOrEmpty(result.Message) ? result.Message : result.Error ?? "Unknown error";
                Snackbar.Add($"Failed to create {typeof(TModel).Name}: {errorMessage}", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void HandleCancel() => NavigationManager.NavigateTo($"{typeof(TModel).Name}s");

    private bool IsFormValid() => EditForm?.IsValid ?? false;

    #endregion
}
