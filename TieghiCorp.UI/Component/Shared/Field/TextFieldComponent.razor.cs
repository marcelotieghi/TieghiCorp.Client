using Microsoft.AspNetCore.Components;

namespace TieghiCorp.UI.Component.Shared.Field;

public partial class TextFieldComponent : ComponentBase
{
    private string InputValue { get; set; } = string.Empty;

    [Parameter]
    public string Label { get; set; } = string.Empty;
}