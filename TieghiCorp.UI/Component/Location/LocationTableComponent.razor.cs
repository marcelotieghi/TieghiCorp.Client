using TieghiCorp.UI.Component.Shared;

namespace TieghiCorp.UI.Component.Location;

public partial class LocationTableComponent : TableComponent<Core.Models.Location>
{
    private TableComponent<Core.Models.Location>? locationTableRef;

    private new async Task ReloadTable()
    {
        if (locationTableRef != null)
            await locationTableRef.ReloadTable();
    }

    private new async Task OnSearch(string text)
    {
        if (locationTableRef != null)
            await locationTableRef.OnSearch(text);
    }
}