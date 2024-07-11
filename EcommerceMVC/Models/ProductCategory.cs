using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models
{
	public class ProductCategory
	{
		public ProductCategory()
		{
			this.Products = new List<Product>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		public int Id { get; set; }

		public string CategoryName { get; set; }

		public string Description { get; set; }

		public int NumberOfProduct { get; set; }
		public string? Image { get; set; }

		public virtual ICollection<Product> Products { get; set; }
	}
}
