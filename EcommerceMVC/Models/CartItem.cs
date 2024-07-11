using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMVC.Models
{
	public class CartItem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		public int Id { get; set; }

		public string Name { get; set; }
		public int price { get; set; }

		public int Quantity { get; set; }

		public string IsOUtOfStock { get; set; }

		public DateTime AddDate { get; set; }
		[ForeignKey("Cart")]
		public int CartId { get; set; }

		public virtual Cart? Cart { get; set; }

		
	}
}
