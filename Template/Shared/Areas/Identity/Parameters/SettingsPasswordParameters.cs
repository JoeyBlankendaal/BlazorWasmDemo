using System.ComponentModel.DataAnnotations;

namespace Template.Shared.Areas.Identity.Parameters;

public class SettingsPasswordParameters
{
    public string UserId { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "EnterAPassword")]
    public string CurrentPassword { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "EnterAPassword")]
    public string NewPassword { get; set; }

    [Compare(nameof(NewPassword), ErrorMessage = "ThosePasswordsDidntMatch")]
    [DataType(DataType.Password)]
    public string NewPasswordConfirmation { get; set; }
}
