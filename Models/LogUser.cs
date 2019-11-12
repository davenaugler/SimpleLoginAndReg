using System.ComponentModel.DataAnnotations;
using System;

namespace SimpleLoginAndReg.Models
{
  public class LogUser
  {
    [Required(ErrorMessage = "Email Address required")]
    [EmailAddress]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Password required")]
    [MinLength(8, ErrorMessage = "Needs at least 8 characters")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}