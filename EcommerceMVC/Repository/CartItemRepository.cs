using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.ViewModel;

namespace EcommerceMVC.Repository
{
	public class CartItemRepository
	{
		private readonly AppDbContext _db;
		public CartItemRepository(AppDbContext db)
		{
			_db = db;
		}

		public CartItem AddToCart(CartItemViewModel model, int CartId)
		{
			var newCartItem = new CartItem()
			{
				Name = model.Name,
				price = model.price,
				Quantity = model.Quantity,
				IsOUtOfStock = model.IsOUtOfStock,
			};
			newCartItem.AddDate = DateTime.Now;
			newCartItem.CartId = CartId;
			var cart = _db.Carts.Where(c => c.Id == CartId).FirstOrDefault();
			cart.CartItems.Add(newCartItem);
			cart.Quentity = cart.Quentity + 1;
			cart.SubTotal = cart.SubTotal + newCartItem.price;
			_db.CartItems.Add(newCartItem);
			_db.SaveChanges();
			return newCartItem;
		}
		public IEnumerable<CartItem> GetAllitemsByCartId(int cardid)
		{
			var listcartitems = _db.CartItems.Where(ct => ct.CartId == cardid).ToList();
			return listcartitems;
		}
		public IEnumerable<CartItem> GetAllCartItems()
		{
			var listcartitems = _db.CartItems.ToList();
			return listcartitems;
		}
		public void RemoveCartItem(int cartitemId)
		{
			var cartitem = _db.CartItems.Where(ct => ct.Id == cartitemId).FirstOrDefault();
			_db.CartItems.Remove(cartitem);
			_db.SaveChanges();
		}

		public void AddMore(int id)
		{
			var item = _db.CartItems.Where(ct => ct.Id == id).FirstOrDefault();
			int priceOne = item.price / item.Quantity;
			item.Quantity = item.Quantity + 1;
			item.price = item.price + priceOne;
			_db.SaveChanges();
		}

		public void RemoveOne(int id)
		{
			var item = _db.CartItems.Where(ct => ct.Id == id).FirstOrDefault();
			if (item.Quantity == 1)
			{
				RemoveCartItem(id);
			}
			else
			{
				int priceOne = item.price / item.Quantity;
				item.Quantity = item.Quantity - 1;
				item.price = item.price - priceOne;
			}
			_db.SaveChanges();
		}
	}
}
