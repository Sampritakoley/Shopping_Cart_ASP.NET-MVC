using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
	public class CartController : Controller
	{
		private readonly CartRepository _cartRepository;
		
		public CartController(CartRepository cartRepository)
		{
			_cartRepository = cartRepository;
		}
		public IActionResult Index()
		{
			int userid = Convert.ToUInt16(TempData["userId"]);
			TempData.Keep("userId");
			IEnumerable<Cart> cartList = _cartRepository.GetAll(userid);   //model
			return View(cartList);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(CartViewModel model)
		{
			int userid = Convert.ToUInt16(TempData["userId"]);
			TempData.Keep("userId");
			var cart=_cartRepository.CreateCart(model,userid);
			return RedirectToAction("Index","Cart");
		}
		
	}
}
