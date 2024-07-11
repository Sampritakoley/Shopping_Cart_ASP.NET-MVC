using EcommerceMVC.Models;

namespace EcommerceMVC.ViewModel
{
	public class OrderViewModel
	{ 
		public List<CartItem> CartItems { get; set; }
		public string Email { get; set; }

		public int OrderPrice { get; set; }
		public DateTime OrderDate { get; set; }

		public string Status { get; set; }

		public DateTime DeliveryDate { get; set; }
	}
}
