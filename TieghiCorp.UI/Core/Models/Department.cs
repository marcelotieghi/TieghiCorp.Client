using System.ComponentModel.DataAnnotations;
using TieghiCorp.UI.Core.Models.Abstract;

namespace TieghiCorp.UI.Core.Models;

public sealed record Department : BaseModel
{
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100, ErrorMessage = "The Name field cannot exceed 100 characters.")]
    [MinLength(2, ErrorMessage = "The Name field must be at least 2 characters long.")]
    [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "The Name field must contain only letters.")]
    public string Name { get; set; } = string.Empty;

    public int LocationId => Location?.Id ?? 0;

    public Location? Location { get; set; }

    public override string BaseName
    {
        get => Name;
        set => Name = value;
    }
}