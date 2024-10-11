using System.ComponentModel.DataAnnotations;

namespace Demo.pl.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="First name is required")]
		public string FName { get; set; }
		[Required(ErrorMessage ="Last name is required")]
		public string LName { get; set; }
		[Required(ErrorMessage ="email is required")]
		[EmailAddress(ErrorMessage ="invalid email ")]
		public string Email { get; set; }
		[Required(ErrorMessage = "password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm password is required")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage ="Password Dose not match")]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }
	}
	
}
