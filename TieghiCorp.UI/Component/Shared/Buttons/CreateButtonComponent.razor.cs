using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Component.Shared.Dialog;
using TieghiCorp.UI.Core.Models.Abstract;

namespace TieghiCorp.UI.Component.Shared.Buttons;

public partial class CreateButtonComponent<TModel> : ComponentBase where TModel : BaseModel
{
    #region Paramenters

    [Parameter]
    public string ButtonName { get; set; } = string.Empty;
    [Parameter]
    public bool IsDisable { get; set; } = false;
    [Parameter]
    public EventCallback OnSaveSuccess { get; set; }

    #endregion

    #region Services

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    #endregion

    #region Methods

    private Task<IDialogReference> OpenDialogAsync()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };

        var parameters = new DialogParameters
        {
            { "OnSaveSuccess", OnSaveSuccess }
        };

        return DialogService.ShowAsync<DialogFormComponent<TModel>>("Form", parameters, options);
    }

    #endregion
}