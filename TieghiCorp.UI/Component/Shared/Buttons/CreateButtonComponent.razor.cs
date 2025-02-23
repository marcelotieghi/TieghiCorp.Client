using Microsoft.AspNetCore.Components;

namespace TieghiCorp.UI.Component.Shared.Buttons;

public partial class CreateButtonComponent : ComponentBase
{
    [Parameter]
    public string Href { get; set; } = string.Empty;

    [Parameter]
    public string ButtonName { get; set; } = string.Empty;
}