using Microsoft.AspNetCore.Components;
using TieghiCorp.UI.Component.Shared.Field;

namespace TieghiCorp.UI.Component.Personnel;

public partial class PersonnelFormComponent : ComponentBase
{
    protected Core.Models.Personnel InputModel { get; set; } = new();

    private FormComponent<Core.Models.Personnel>? Form { get; set; }

    private IEnumerable<string> Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            yield return "The field is required";
            yield break;
        }

        if (InputModel.Department != null && !InputModel.Department.Name.Contains(value))
        {
            yield return "This is an incorrect value";
        }
    }
}