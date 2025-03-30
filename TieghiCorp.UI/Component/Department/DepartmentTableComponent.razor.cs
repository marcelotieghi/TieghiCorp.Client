using TieghiCorp.UI.Component.Shared;

namespace TieghiCorp.UI.Component.Department;

public partial class DepartmentTableComponent : TableComponent<Core.Models.Department>
{
    private TableComponent<Core.Models.Department>? departmentTableRef;

    private new async Task ReloadTable()
    {
        if (departmentTableRef != null)
            await departmentTableRef.ReloadTable();
    }

    private new async Task OnSearch(string text)
    {
        if (departmentTableRef != null)
            await departmentTableRef.OnSearch(text);
    }
}