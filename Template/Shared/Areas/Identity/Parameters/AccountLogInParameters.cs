using System.ComponentModel.DataAnnotations;

namespace Template.Shared.Areas.Identity.Parameters;

public class AccountLogInParameters
{
    [EmailAddress(ErrorMessage = "EnterAValidEmail")]
    [Required(ErrorMessage = "EnterAnEmail")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "EnterAPassword")]
    public string Password { get; set; }
}
