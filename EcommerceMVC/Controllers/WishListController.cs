using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace EcommerceMVC.Controllers
{
	public class WishListController : Controller
	{
		private readonly WishListRepository _wishlistRepository;
		private readonly ProductRepository _productRepository;
		private readonly AppDbContext _db;
		public WishListController(WishListRepository wishListRepository, ProductRepository productRepository,AppDbContext db)
		{
			_wishlistRepository = wishListRepository;
			_productRepository = productRepository;
			_db = db;
		}
		public IActionResult Index()
		{
			int userid = Convert.ToUInt16(TempData["userId"]);
			TempData.Keep("userId");
			IEnumerable<WishList> listOfWishList =_wishlistRepository.GetWhislistByUserId(userid);    //model
			return View(listOfWishList);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(WishListViewModel model)
		{
			int userid = Convert.ToUInt16(TempData["userId"]);
			TempData.Keep("userId");
			var wishlist = _wishlistRepository.Create(model, userid);
			return RedirectToAction("Index", "WishList");
		}
		[HttpGet]
		public IActionResult Detail(int id)
		{
			TempData["Wishlistid"] = id;
			TempData.Keep("Wishlistid");
			IEnumerable<Product> listOfProduct=_wishlistRepository.GetProductByWishListId(id);
			return View(listOfProduct);
		}
		[HttpGet]
		public IActionResult Add(int id)
		{
			TempData["wishlistid"] = id;
			TempData.Keep("wishlistid");
			return RedirectToAction("AddToWishList", "WishList");
		}
		public IActionResult AddToWishList()
		{
			int ProductId = Convert.ToInt16(TempData["ProductId"]);
			TempData.Keep("ProductId");
			int Wishlistid = Convert.ToInt16(TempData["wishlistid"]);
			TempData.Keep("wishlistid");
			var product = _productRepository.GetById(ProductId);
			product.WhislistId = Wishlistid;
			_db.SaveChanges();
			return RedirectToAction("Detail", "WishList",new {id= Wishlistid });
		}
		public IActionResult Remove(int id)
		{
			TempData["productId"] = null;
			int Wishlistid = Convert.ToInt16(TempData["wishlistid"]);
			TempData.Keep("wishlistid");
			int Productid = id;
			var product = _productRepository.GetById(Productid);
			product.WhislistId = null;
			_db.SaveChanges();
			return RedirectToAction("Detail", "WishList", new { id = Wishlistid });
		}
		public IActionResult Delete(int id)
		{
			TempData["wishlistid"] = null;
			int wishlistid = id;
			var listofproducts=_db.Products.Where(p=>p.WhislistId== wishlistid).ToList();
			foreach(var product in listofproducts)
			{
				product.WhislistId = null;
				_db.SaveChanges();
			}
			_wishlistRepository.Delete(wishlistid);
			return RedirectToAction("Index");
		}
		
	}
}
