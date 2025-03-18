using System.ComponentModel.DataAnnotations;

namespace E_CommerceApp.ViewModel
{
	public class LoginVM
	{
		[Required(ErrorMessage = "*")]
		public string email { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember Me")]
		public bool RememberMe { get; set; }
	}
}
