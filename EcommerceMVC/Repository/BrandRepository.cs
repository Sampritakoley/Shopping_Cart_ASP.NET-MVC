using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.Services;
using EcommerceMVC.ViewModel;

namespace EcommerceMVC.Repository
{
	public class BrandRepository
	{

		private readonly AppDbContext _db;

		private readonly Photo _photo;
		public BrandRepository(AppDbContext db, Photo photo)
		{
			_db = db;
			_photo = photo;
		}
		public async Task Create(BrandViewModel model)
		{
			var result =await _photo.AddPhoto(model.Logo);
			var newbrand = new Brand()
			{
				Logo = result.Url.ToString(),
				Name = model.Name,
				LaunchDate = DateTime.Now
			};
			_db.Brands.Add(newbrand);
			_db.SaveChanges();
		}
		public Brand SearchByName(string name)
		{
			var brand=_db.Brands.Where(b=>b.Name==name).FirstOrDefault();
			return brand;
		}

		public IEnumerable<Product> GetByBrand(int id)
		{
			var listOfProducts = _db.Products.Where(p => p.BrandId == id).ToList();
			return listOfProducts;
		}

		public IEnumerable<Brand> GetAllBrand()
		{
			var listOfBrand = _db.Brands.ToList();
			return listOfBrand;
		}
	}
}
