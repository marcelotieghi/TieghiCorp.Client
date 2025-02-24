using System.ComponentModel.DataAnnotations;
using TieghiCorp.UI.Core.Models.Abstract;

namespace TieghiCorp.UI.Core.Models;

public sealed record Personnel : BaseModel
{
    [Required(ErrorMessage = "The Firstname field is required.")]
    [StringLength(100, ErrorMessage = "The Firstname field cannot exceed 100 characters.")]
    [MinLength(2, ErrorMessage = "The Firstname field must be at least 2 characters long.")]
    [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "The Firstname field must contain only letters.")]
    public string Firstname { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Lastname field is required.")]
    [StringLength(100, ErrorMessage = "The Lastname field cannot exceed 100 characters.")]
    [MinLength(2, ErrorMessage = "The Lastname field must be at least 2 characters long.")]
    [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "The Lastname field must contain only letters.")]
    public string Lastname { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Email field is required.")]
    [StringLength(255, ErrorMessage = "The Email field cannot exceed 255 characters.")]
    [EmailAddress(ErrorMessage = "The Email field must be a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Job Title field is required.")]
    [StringLength(100, ErrorMessage = "The Job Title field cannot exceed 100 characters.")]
    [MinLength(2, ErrorMessage = "The Job Title field must be at least 2 characters long.")]
    [RegularExpression("^[a-zA-ZÀ-ÿ' ]*$", ErrorMessage = "The Job Title field must contain only letters, spaces, and apostrophes.")]
    public string JobTitle { get; set; } = string.Empty;

    public int DepartmentId => Department?.Id ?? 0;

    public Department? Department { get; set; }

    public string Fullname => $"{Firstname} {Lastname}".Trim();

    public override string BaseName
    {
        get => Email;
        set => Email = value;
    }
}