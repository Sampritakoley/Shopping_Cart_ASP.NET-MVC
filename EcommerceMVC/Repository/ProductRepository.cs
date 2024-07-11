using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.Services;
using EcommerceMVC.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Repository
{
	public class ProductRepository
	{
		private readonly AppDbContext _db;
		private readonly Photo _photo;
		private readonly BrandRepository _brandRepository;
		public ProductRepository(AppDbContext db, Photo photo, BrandRepository brandRepository)
		{
			_photo = photo;
			_db = db;
			_brandRepository = brandRepository;
		}
		public async Task Create(ProductViewModel model,int cate_id)
		{
			var result = await _photo.AddPhoto(model.Image);
			string brandname = model.BrandName;
			var brand=_brandRepository.SearchByName(brandname);
			var newProduct = new Product()
			{
				Image = result.Url.ToString(),
				ManufacturingDate = DateTime.Now,
				Name = model.Name,
				BrandName = model.BrandName,
				Price = model.Price,
				NoOfProduct = model.NoOfProduct,
				Availability = model.Availability,
				ProductCategoryId = cate_id,
				BrandId = brand.id
			};
			_db.Products.Add(newProduct);
			var category=_db.ProductCategorys.Where(c=>c.Id==cate_id).FirstOrDefault();
			category.NumberOfProduct = category.NumberOfProduct+1;
			category.Products.Add(newProduct);
			await _db.SaveChangesAsync();
		}
		public IEnumerable<Product> GetAll()
		{
			return _db.Products.ToList();
		}
		public Product GetById(int id)
		{
			return _db.Products.Where(p => p.Id == id).FirstOrDefault();
		}
		public void DeleteById(int id)
		{
			var product = GetById(id);
			_db.Products.Remove(product);
			_db.SaveChanges();
		}
		public IEnumerable<Product> SearchByName(string name)
		{
			var productlist = _db.Products.Where(p => p.Name == name).ToList();
			return productlist;
		}
		public IEnumerable<Product> FilterByPriceRange(PriceParameter model)
		{
			var productlist = _db.Products.Where(p => p.Price <= model.PriceRange).ToList();
			return productlist;
		}
	}
}
