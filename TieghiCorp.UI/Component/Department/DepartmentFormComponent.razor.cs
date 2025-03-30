using TieghiCorp.UI.Component.Shared.Field;

namespace TieghiCorp.UI.Component.Department;

public partial class DepartmentFormComponent : FormComponent<Core.Models.Department>
{
    private Core.Models.Department InputModel { get; set; } = new();

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