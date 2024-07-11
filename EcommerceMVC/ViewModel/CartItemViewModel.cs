namespace EcommerceMVC.ViewModel
{
	public class CartItemViewModel
	{
		public int price { get; set; }
		public string Name { get; set; }

		public int Quantity { get; set; }

		public string IsOUtOfStock { get; set; }
	}
}
