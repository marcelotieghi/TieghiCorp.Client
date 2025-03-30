using Microsoft.AspNetCore.Components;
using TieghiCorp.UI.Core.Models.Abstract;

namespace TieghiCorp.UI.Component.Shared.Dialog;

public partial class DialogFormComponent<TModel> where TModel : BaseModel
{
    [Parameter]
    public EventCallback OnSaveSuccess { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }
}