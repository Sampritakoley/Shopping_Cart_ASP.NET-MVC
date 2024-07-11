using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMVC.Models
{
	public class OrderDetail
	{
		public OrderDetail()
		{
			this.OrderItems = new List<OrderItem>();
		}
		public int Id { get; set; }

		public string Email { get; set; }

		public DateTime OrderDate { get; set;}
		public int OrderPrice { get; set;}

		public DateTime DeliveryDate { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }

		public virtual User? User { get; set; }

		public virtual ICollection<OrderItem> OrderItems { get; set; }
	}
}
