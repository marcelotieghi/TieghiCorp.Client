using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace TieghiCorp.UI.Component.Shared.Dialog;

public partial class DialogConfirmationComponent : ComponentBase
{
    [CascadingParameter]
    private IMudDialogInstance? MudDialog { get; set; }

    [Parameter]
    public EventCallback OnConfirm { get; set; }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    [Parameter]
    public string ContentText { get; set; } = string.Empty;

    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public string ButtonText { get; set; } = "Yes";

    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    private async Task Submit()
    {
        if (OnConfirm.HasDelegate)
        {
            await OnConfirm.InvokeAsync();
        }
        MudDialog?.Close(DialogResult.Ok(true));
    }

    private async Task Cancel()
    {
        if (OnCancel.HasDelegate)
        {
            await OnCancel.InvokeAsync();
        }
        MudDialog?.Cancel();
    }
}