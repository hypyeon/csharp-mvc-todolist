using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [EmailAddress]
    // this annotation handles validating input associated with the property to ensure email address format expectation 
    [Display(Name = "Email Address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    // this annotation allows specification on how data should look or be formatted more precisely than a conventional C# type like `string`
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$", ErrorMessage = "Your password must contain at least six characters, a capital letter, a lowercase letter, a number, and a special character.")]
    // password requirements 
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    // Display annotation allows specification on a different way for property to be displayed 
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    // telling program to compare 2 properties and return an error if not matching 
    public string ConfirmPassword { get; set; }
  }
}