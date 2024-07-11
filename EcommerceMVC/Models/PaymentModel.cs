using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMVC.Models
{
	public class PaymentModel
	{
		public PaymentModel()
		{
			this.Orders = new HashSet<Order>();
		}
		[Key]
		public int Id { get; set; }

		public string TransactionId { get; set; }

		public string Status { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }

		public User User { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
	}
}
