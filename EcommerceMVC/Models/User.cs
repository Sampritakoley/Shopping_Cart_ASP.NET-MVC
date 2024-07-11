using EcommerceMVC.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMVC.Models
{
	public class User
	{
		public User()
		{
			this.Addresses = new HashSet<Address>();
			this.Carts = new HashSet<Cart>();
			this.WishLists=new HashSet<WishList>();
			this.Orders=new HashSet<Order>();
			this.PaymentModels = new HashSet<PaymentModel>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }
  

        public string PhoneNumber { get; set; }

		public bool IsPrime { get; set; }
		public int NoOfTransaction { get; set; }

		public GenderType GenderType { get; set; }

		public string DateOfBirth { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public RoleCategory RoleCategory { get; set; }

		public Profile Profile { get; set; }
		
		public virtual ICollection<Address> Addresses { get; set; }

		public virtual ICollection<WishList> WishLists { get; set; }

		public virtual ICollection<Cart> Carts { get; set; }

		public virtual ICollection<Order> Orders { get; set; }

		public virtual ICollection<PaymentModel> PaymentModels { get; set; }
	}
}
