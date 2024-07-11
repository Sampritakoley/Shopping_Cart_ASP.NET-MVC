using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMVC.Models
{
	public class Order
	{
		public int Id { get; set; }

		public DateTime OrderDate { get; set; }

		public string OrderStatus { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		[ForeignKey("PaymentModel")]
		public int Paymentid { get; set; }
	}
}
