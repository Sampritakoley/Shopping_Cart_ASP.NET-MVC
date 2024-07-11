using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Repository
{
	public class CartRepository
	{
		private readonly AppDbContext _db;
		public CartRepository(AppDbContext db)
		{
			_db = db;
		}

		public void AddNewCart(CartViewModel model,int userid)
		{
			var newCart = new Cart()
			{
				CreatedDate = DateTime.Now,
				Name = model.Name,
			};
			newCart.UserId = userid;
			newCart.Quentity = 0;
			newCart.SubTotal = 0;
			var user=_db.Users.Where(u=>u.Id==userid).FirstOrDefault();
			user.Carts.Add(newCart);
		}
		public Cart CreateCart(CartViewModel cart,int userid)
		{
			var newcart = new Cart()
			{
				Name = cart.Name,
				CreatedDate = DateTime.Now,
				UserId = userid,
				Quentity = 0,
				SubTotal=0
			};
			_db.Carts.Add(newcart);
			_db.SaveChanges();
			return newcart;
		}
		public IEnumerable<Cart> GetAll(int id)
		{
			return _db.Carts.Where(c => c.UserId == id).ToList();
		}
		public void DeleteById(int id)
		{
			var cart=GetById(id);
			_db.Carts.Remove(cart);
			_db.SaveChanges();
		}
		public Cart GetById(int id)
		{
			var cart = _db.Carts.Where(c => c.Id == id).FirstOrDefault();
			return cart;
		}
		public IEnumerable<CartItem> ShowAllCartItems(int cartid)
		{
			var cart = _db.Carts.Where(c => c.Id == cartid).FirstOrDefault();
			var listOfCartItem = cart.CartItems.ToList();
			return listOfCartItem;
		}
	}
}
