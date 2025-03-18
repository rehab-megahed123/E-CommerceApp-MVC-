using System.ComponentModel.DataAnnotations;

namespace E_CommerceApp.ViewModel
{
	public class RegisterViewModel
	{
		public string email { get; set; }
		public string FullName { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Compare("Password")]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }
        public string Role { get; set; }

    }
}
