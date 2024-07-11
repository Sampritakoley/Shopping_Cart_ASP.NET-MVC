using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Repository
{
	public class WishListRepository
	{
		private readonly AppDbContext _db;
		public WishListRepository(AppDbContext db)
		{
			_db = db;
		}
		public WishList Create(WishListViewModel model,int userid)
		{
			var newWishlist = new WishList()
			{
				Name = model.Name,
			};
			newWishlist.CreatedDate = DateTime.Now;
			newWishlist.UserId= userid;
			var user = _db.Users.Where(u => u.Id == userid).FirstOrDefault();
			user.WishLists.Add(newWishlist);
			_db.SaveChanges();
			return newWishlist;
		}
		public void Delete(int id)
		{
			var wishlist=_db.WishLists.Where(w=>w.Id==id).FirstOrDefault();
			_db.WishLists.Remove(wishlist);
			_db.SaveChanges();
		}
		public WishList GetById(int id)
		{
			var wishlist = _db.WishLists.Where(w => w.Id == id).FirstOrDefault();
			return wishlist;
		}
		public IEnumerable<Product> ShowAllProduct(int id)
		{
			var wishlist = GetById(id);
			var productList = wishlist.Products.ToList();
			return productList;
		}
		public IEnumerable<WishList> GetWhislistByUserId(int id)
		{
			var listWishlist = _db.WishLists.Where(w => w.UserId == id).ToList();
			return listWishlist;
		}
		public IEnumerable<Product> GetProductByWishListId(int id)
		{
			var listOfProduct = _db.Products.Where(p => p.WhislistId == id).ToList();
			return listOfProduct;
		}
	}
}
