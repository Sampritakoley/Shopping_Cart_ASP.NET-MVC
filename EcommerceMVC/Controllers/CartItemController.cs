using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using EcommerceMVC.Services;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
	public class CartItemController : Controller
	{
		private readonly CartItemRepository _cartItemRepository;
		private readonly ProductRepository _productRepository;
		private readonly CartRepository _cartRepository;
		public CartItemController(CartItemRepository cartItemRepository, ProductRepository productRepository, CartRepository cartRepository)
		{
			_cartItemRepository = cartItemRepository;
			_productRepository = productRepository;
			_cartRepository = cartRepository;
		}
		[HttpGet]
		public IActionResult Index()
		{
			int cartid = Convert.ToInt16(TempData["cartid"]);
			TempData.Keep("cartid");
			IEnumerable<CartItem> cartitemList = _cartItemRepository.GetAllitemsByCartId(cartid);
			List<CartItem> listCartItem=new List<CartItem>();
			foreach(CartItem item in cartitemList)
			{
				listCartItem.Add(item);
			}
			TempData.Put("listofCartItem", listCartItem);
			return View(cartitemList);
		}
		[HttpGet]
		public IActionResult Add(int id)
		{
			TempData["cartid"] = id;
			TempData.Keep("cartid");
			return RedirectToAction("AddToCart", "CartItem");
		}
		[HttpGet]
		public IActionResult AddToCart()
		{
			int ProductId = Convert.ToInt16(TempData["ProductId"]);
			TempData.Keep("ProductId");
			var product = _productRepository.GetById(ProductId);
			var model = new CartItemViewModel()
			{
				price = product.Price,
				Name = product.Name,
				Quantity = 1,
				IsOUtOfStock = product.NoOfProduct>0? "False":"True"
			};
			int cartid = Convert.ToInt16(TempData["cartid"]);
			TempData.Keep("cartid");
			var cartitem = _cartItemRepository.AddToCart(model, cartid);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult ViewAll(int id)
		{
			TempData["cartid"] = id;
			TempData.Keep("cartid");
			return RedirectToAction("Index");
	    }
		public IActionResult Remove(int id)
		{
			_cartItemRepository.RemoveCartItem(id);
			return RedirectToAction("Index");
		}
		public IActionResult AddMore(int id)
		{
			_cartItemRepository.AddMore(id);
			return RedirectToAction("Index");
		}

		public IActionResult RemoveOne(int id)
		{
			_cartItemRepository.RemoveOne(id);
			return RedirectToAction("Index");
		}
	}
}
