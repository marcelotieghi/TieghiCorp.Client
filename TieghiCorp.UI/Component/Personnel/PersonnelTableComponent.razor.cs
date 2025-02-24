using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Component.Shared;
using TieghiCorp.UI.Core.Services;

namespace TieghiCorp.UI.Component.Personnel;

public partial class PersonnelTableComponent : TableComponent<Core.Models.Personnel>
{
    #region Properties

    protected string SearchString = string.Empty;
    protected TableComponent<Core.Models.Personnel>? Table { get; set; }

    #endregion

    #region Services

    [Inject]
    private IServices<Core.Models.Personnel> Services { get; set; } = null!;

    #endregion

    #region Methods

    protected async Task<TableData<Core.Models.Personnel>> LoadPersonnelData(TableState state, CancellationToken token)
    {
        try
        {
            var result = await Services.ListAsync(
                "v1/personnel",
                state.Page + 1,
                state.PageSize,
                SearchString,
                state.SortLabel,
                state.SortDirection == SortDirection.Descending ? "desc" : "asc",
                token);

            return new TableData<Core.Models.Personnel>
            {
                TotalItems = result.TotalCount,
                Items = result.Data
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during ServerReload: {ex.Message}");
            return new TableData<Core.Models.Personnel> { TotalItems = 0, Items = [] };
        }
    }

    public async Task ReloadTable()
    {
        if (Table?.TableRef != null)
        {
            await Table.TableRef.ReloadServerData();
        }
    }

    #endregion
}