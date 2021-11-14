using System.ComponentModel.DataAnnotations;

namespace Template.Shared.Areas.Identity.Parameters;

public class AccountCreateParameters
{
    [Required(ErrorMessage = "EnterAUserName")]
    public string UserName { get; set; }

    [EmailAddress(ErrorMessage = "EnterAValidEmail")]
    [Required(ErrorMessage = "EnterAnEmail")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "EnterAPassword")]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "ThosePasswordsDidntMatch")]
    [DataType(DataType.Password)]
    public string PasswordConfirmation { get; set; }
}
