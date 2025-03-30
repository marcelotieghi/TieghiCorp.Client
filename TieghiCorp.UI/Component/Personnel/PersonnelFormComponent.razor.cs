using TieghiCorp.UI.Component.Shared.Field;

namespace TieghiCorp.UI.Component.Personnel;

public partial class PersonnelFormComponent : FormComponent<Core.Models.Personnel>
{
    private Core.Models.Personnel InputModel { get; set; } = new();

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
