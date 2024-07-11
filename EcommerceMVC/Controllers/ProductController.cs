using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
	public class ProductController : Controller
	{
		private readonly ProductRepository _productRepo;

		public ProductController(ProductRepository productRepo)
		{
			_productRepo = productRepo;
		}
		public async Task<IActionResult> Index()
		{
			IEnumerable<Product> productList =_productRepo.GetAll();    //model
			return View(productList);
		}
		[Authorize(Roles ="Admin")]
		public IActionResult Create()
		{
			int cateid = Convert.ToInt16(TempData["categoryId"]);
			TempData.Keep("categoryId");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(ProductViewModel model)
		{
			int cateid = Convert.ToInt16(TempData["categoryId"]);
			TempData.Keep("categoryId");
			if (ModelState.IsValid)
			{
				await _productRepo.Create(model,cateid);
				return RedirectToAction("ShowAll","ProductCategory",new {id= cateid });
			}
			return View(model);
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			_productRepo.DeleteById(id);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> AddToCart(int id)
		{
			int Productid = id;
			TempData["productId"] = id;
			TempData.Keep("productId");
			return RedirectToAction("Index", "Cart");
		}
		[HttpGet]
		public async Task<IActionResult> AddToWishList(int id)
		{
			int Productid = id;
			TempData["productId"] = id;
			TempData.Keep("productId");
			return RedirectToAction("Index", "WishList");
		}

		[HttpGet]
		public async Task<ActionResult> FindProductByPriceRange(PriceParameter pricemodel)
		{
			if(!pricemodel.ValidRange(pricemodel.PriceRange))
			{
				ViewBag.ErrorMessage = "Enter pricerange below 10000";
				return View();
			}
			var res = _productRepo.FilterByPriceRange(pricemodel);
			return View(res);
		}
	}
}
