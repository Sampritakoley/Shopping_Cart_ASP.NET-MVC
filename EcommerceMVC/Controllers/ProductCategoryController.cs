using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
	public class ProductCategoryController : Controller
	{
		private readonly ProductCategoryRepository _productCategoryRepo;

		public ProductCategoryController(ProductCategoryRepository productCategoryRepo)
		{
			_productCategoryRepo = productCategoryRepo;
		}
        public async Task<IActionResult> Index()    //controller
        {
            IEnumerable<ProductCategory> categoryList = await _productCategoryRepo.GetAll();   //model
            return View(categoryList);    //view
        }
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize(Roles ="Admin")]
		[HttpPost]
		public async Task<IActionResult> Create(ProductCategoryViewModel model)
		{
			if (ModelState.IsValid)
			{
				await _productCategoryRepo.Create(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> GetById(int id)
		{
			var res=_productCategoryRepo.GetById(id);
			return View(res);
		}

		[HttpGet]
		public async Task<IActionResult> ShowAll(int id)
		{
			TempData["categoryId"] = id;
			var listOfProduct = _productCategoryRepo.ShowAllProduct(id);
			return View(listOfProduct);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteById(int id)
		{
			_productCategoryRepo.DeleteById(id);
			return RedirectToAction("Index");
		}
	}
}
