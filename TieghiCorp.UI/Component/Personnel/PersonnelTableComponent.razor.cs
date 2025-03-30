using TieghiCorp.UI.Component.Shared;

namespace TieghiCorp.UI.Component.Personnel;

public partial class PersonnelTableComponent : TableComponent<Core.Models.Personnel>
{
    private TableComponent<Core.Models.Personnel>? personnelTableRef;

    private new async Task ReloadTable()
    {
        if (personnelTableRef != null)
            await personnelTableRef.ReloadTable();
    }

    private new async Task OnSearch(string text)
    {
        if (personnelTableRef != null)
            await personnelTableRef.OnSearch(text);
    }
}