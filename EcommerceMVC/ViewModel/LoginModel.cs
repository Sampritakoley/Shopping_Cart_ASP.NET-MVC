using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.ViewModel
{
	public class LoginModel
	{
		[Required]
		[EmailAddress]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool KeepMeLoggedIn { get; set; }
	}
}
