using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models
{
	public class OrderItem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		public int Id { get; set; }

		public string Name { get; set; }
		public int price { get; set; }

		public int Quantity { get; set; }

		[ForeignKey("Order")]
		public int? OrderId { get; set; }

		public virtual OrderDetail? Order { get; set; }
	}
}
