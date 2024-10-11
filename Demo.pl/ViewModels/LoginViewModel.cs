using System.ComponentModel.DataAnnotations;

namespace Demo.pl.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "email is required")]
		[EmailAddress(ErrorMessage = "invalid email ")]
		public string Email { get; set; }
		[Required(ErrorMessage = "password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}
