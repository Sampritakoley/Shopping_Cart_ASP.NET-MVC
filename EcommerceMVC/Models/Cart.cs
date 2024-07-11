using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models
{
	public class Cart
	{
		public Cart()
		{
			this.CartItems = new HashSet<CartItem>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		public int Id { get; set; }

		public DateTime CreatedDate { get; set; }
		public string Name { get; set; }
		[ForeignKey("User")]
		public int? UserId { get; set; }

		public int? SubTotal { get; set; }

		public int? Quentity { get; set; }

		public virtual ICollection<CartItem> CartItems { get; set; }
	}
}
