using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
	public class BrandController : Controller
	{
		private readonly BrandRepository _brandRepository;

		public BrandController(BrandRepository brandRepository)
		{
			_brandRepository=brandRepository;
		}
		public IActionResult Index()
		{
			IEnumerable<Brand> brandList = _brandRepository.GetAllBrand();  //model
			return View(brandList);
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]

		public async Task<IActionResult> Create(BrandViewModel model)
		{
			if(ModelState.IsValid)
			{
				await _brandRepository.Create(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public IActionResult SearchByName(string name)
		{
			var brand = _brandRepository.SearchByName(name);
			return RedirectToAction("Detail", "Brand", new { id = brand.id });
		}
		public IActionResult Detail(int id)
		{
			var productlist = _brandRepository.GetByBrand(id);
			return View(productlist);
		}
	}
}
