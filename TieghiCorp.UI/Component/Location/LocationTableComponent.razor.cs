using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Component.Shared;
using TieghiCorp.UI.Core.Services;

namespace TieghiCorp.UI.Component.Location;

public partial class LocationTableComponent : TableComponent<Core.Models.Location>
{
    #region Properties

    protected string SearchString = string.Empty;
    protected TableComponent<Core.Models.Location>? Table { get; set; }

    #endregion

    #region Services

    [Inject]
    private IServices<Core.Models.Location> Services { get; set; } = null!;

    #endregion

    #region Methods

    protected async Task<TableData<Core.Models.Location>> LoadLocationData(TableState state, CancellationToken token)
    {
        try
        {
            var result = await Services.ListAsync(
                "v1/locations",
                state.Page + 1,
                state.PageSize,
                SearchString,
                state.SortLabel,
                state.SortDirection == SortDirection.Descending ? "desc" : "asc",
                token);

            return new TableData<Core.Models.Location>
            {
                TotalItems = result.TotalCount,
                Items = result.Data
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during ServerReload: {ex.Message}");
            return new TableData<Core.Models.Location> { TotalItems = 0, Items = [] };
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