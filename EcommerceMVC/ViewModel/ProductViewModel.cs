namespace EcommerceMVC.ViewModel
{
	public class ProductViewModel
	{
		public DateTime ManufacturingDate { get; set; }

		public string Name { get; set; }

		public int NoOfProduct { get; set; }
		public string BrandName { get; set; }
		public int Price { get; set; }

		public string Availability { get; set; }

		public IFormFile Image { get; set; }

	}
}
