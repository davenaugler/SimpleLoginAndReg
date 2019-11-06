using System.ComponentModel.DataAnnotations;

namespace SimpleLoginAndReg.Models
{
  public class RegUser
  {
    [Required(ErrorMessage = "First Name required")]
    [MinLength(2, ErrorMessage = "Needs at least 2 characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name required")]
    [MinLength(2, ErrorMessage = "Needs at least 2 characters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email Address required")]
    [EmailAddress]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Password required")]
    [MinLength(8, ErrorMessage = "Needs at least 8 characters")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [MinLength(8, ErrorMessage = "Needs at least 8 characters")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

  }
}