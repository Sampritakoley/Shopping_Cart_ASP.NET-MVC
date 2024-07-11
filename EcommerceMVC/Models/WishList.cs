using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models
{
	public class WishList
	{
		public WishList()
		{
			this.Products = new HashSet<Product>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		public int Id { get; set; }

		public DateTime CreatedDate { get; set; }

		public string Name { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }

		public virtual ICollection<Product> Products { get; set; }

	}
}
