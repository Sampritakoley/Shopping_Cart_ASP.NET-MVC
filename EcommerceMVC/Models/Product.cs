using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMVC.Models
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]

		public int Id { get; set; }

		public DateTime ManufacturingDate { get; set; }

		public string Name { get; set; }

		public int Price { get; set; }

		public int NoOfProduct { get; set; }

		public string Availability { get; set; }

		public string BrandName { get; set; }

		public string? Image { get; set; }

		[ForeignKey("ProductCategory")]
		public int ProductCategoryId { get; set; }

		public virtual ProductCategory? ProductCategory { get; set; }

		[ForeignKey("Whislist")]
		public int? WhislistId { get; set; }

		public virtual WishList? Whislist { get; set; }

		[ForeignKey("Brand")]
		public int? BrandId { get; set; }

		public virtual Brand? Brand { get; set; }
	}
}
