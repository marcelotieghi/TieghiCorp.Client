using TieghiCorp.UI.Component.Shared.Field;

namespace TieghiCorp.UI.Component.Location;

public partial class LocationFormComponent : FormComponent<Core.Models.Location>
{
    protected Core.Models.Location InputModel { get; set; } = new();
}