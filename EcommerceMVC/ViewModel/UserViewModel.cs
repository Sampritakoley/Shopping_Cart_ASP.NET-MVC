using EcommerceMVC.Enum;
using EcommerceMVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMVC.ViewModel
{
	public class UserViewModel
	{
		[Required]
		public string FirstName { get; set; }

		public string LastName { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string PhoneNumber { get; set; }



        public GenderType GenderType { get; set; }

		public string DateOfBirth { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public RoleCategory RoleCategory { get; set; }
		public Address? Address { get; set; }
	}
}
