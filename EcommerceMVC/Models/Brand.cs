using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models
{
	public class Brand
	{

		public Brand()
		{
			this.Products = new HashSet<Product>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		public int id { get; set; }

		public string Name { get; set; }
		public string? Logo { get; set; }

		public DateTime LaunchDate { get; set; }

		public virtual ICollection<Product> Products { get; set; }
	}
}
