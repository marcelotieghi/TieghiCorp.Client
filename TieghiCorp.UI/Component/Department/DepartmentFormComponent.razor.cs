using Microsoft.AspNetCore.Components;
using TieghiCorp.UI.Component.Shared.Field;

namespace TieghiCorp.UI.Component.Department;

public partial class DepartmentFormComponent : ComponentBase
{
    protected Core.Models.Department InputModel { get; set; } = new();

    private FormComponent<Core.Models.Department>? Form { get; set; }

    private IEnumerable<string> Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            yield return "The field is required";
            yield break;
        }

        if (InputModel.Location != null && !InputModel.Location.Name.Contains(value))
        {
            yield return "This is an incorrect value";
        }
    }
}