using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Component.Shared.Buttons;
using TieghiCorp.UI.Core.Models.Abstract;

namespace TieghiCorp.UI.Component.Shared;

public partial class TableComponent<TModel> : EditButtonComponent<TModel> where TModel : BaseModel
{
    [Parameter]
    public required RenderFragment ToolBarTemplate { get; set; }

    [Parameter]
    public required RenderFragment HeaderTemplate { get; set; }

    [Parameter]
    public required RenderFragment<TModel> RowTemplate { get; set; }

    [Parameter]
    public required RenderFragment<TModel> RowEditingTemplate { get; set; }

    [Parameter]
    public required RenderFragment<EditButtonContext> ButtonContent { get; set; }

    [Parameter]
    public Func<TableState, CancellationToken, Task<TableData<TModel>>>? ServerReloadAsync { get; set; }

    [Parameter]
    [Category("Editing")]
    public required Action<object?> RowEditPreview { get; set; }

    [Parameter]
    [Category("Editing")]
    public required Action<object?> RowEditCancel { get; set; }

    [Parameter]
    [Category("Editing")]
    public required Action<object?> RowEditCommit { get; set; }

    public MudTable<TModel>? TableRef { get; set; }
}