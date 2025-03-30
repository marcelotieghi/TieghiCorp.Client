using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Component.Shared.Buttons;
using TieghiCorp.UI.Core.Api;
using TieghiCorp.UI.Core.Models.Abstract;
using TieghiCorp.UI.Core.Services;

namespace TieghiCorp.UI.Component.Shared;

public partial class TableComponent<TModel> : EditButtonComponent<TModel> where TModel : BaseModel
{
    #region Paramenters

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

    #endregion

    #region Properties

    private MudTable<TModel>? MudTableRef { get; set; }
    protected bool IsLoading { get; set; }

    protected string SearchString = string.Empty;

    #endregion

    #region Services

    [Inject]
    protected IServices<TModel> Services { get; set; } = null!;

    #endregion

    #region Methods

    protected async Task<TableData<TModel>> LoadData(TableState state, CancellationToken token)
    {
        try
        {
            IsLoading = true;

            var result = await Services.ListAsync(
                ApiRoutes.GetUrl<TModel>(),
                state.Page + 1,
                state.PageSize,
                SearchString,
                state.SortLabel,
                state.SortDirection == SortDirection.Descending ? "desc" : "asc",
                token);

            return new TableData<TModel>
            {
                TotalItems = result.TotalCount,
                Items = result.Data
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during ServerReload: {ex.Message}");
            return new TableData<TModel> { TotalItems = 0, Items = [] };
        }
        finally
        {
            IsLoading = false;
        }
    }

    public virtual async Task ReloadTable()
    {
        await InvokeAsync(async () =>
        {
            if (MudTableRef != null)
            {
                await MudTableRef.ReloadServerData();
            }
            StateHasChanged();
        });
    }

    public async Task OnSearch(string text)
    {
        SearchString = text;
        await ReloadTable();
    }

    #endregion
}