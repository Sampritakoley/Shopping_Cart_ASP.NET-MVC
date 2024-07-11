using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.Services;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Repository
{
	public class ProductCategoryRepository
	{
		private readonly AppDbContext _db;
		private readonly Photo _photo;
		public ProductCategoryRepository(AppDbContext db, Photo photo)
		{
			_photo = photo;
			_db = db;
		}

		public async Task Create(ProductCategoryViewModel model)
		{
			var result =await _photo.AddPhoto(model.Image);
			var newProductCategory = new ProductCategory()
			{
				Image = result.Url.ToString(),
				CategoryName = model.CategoryName,
				Description = model.Description
			};
			_db.ProductCategorys.Add(newProductCategory);
			await _db.SaveChangesAsync();
		}
        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await _db.ProductCategorys.ToListAsync();
        }
		public ProductCategory GetById(int id)
		{
			return  _db.ProductCategorys.Where(pc=>pc.Id== id).FirstOrDefault();
		}
		public IEnumerable<Product> ShowAllProduct(int id)
		{
			//var cate = GetById(id);
			var productList=_db.Products.Where(p=>p.ProductCategoryId==id).ToList();
			return productList;
		}
		public void DeleteById(int id)
		{
			var cate= GetById(id);
			_db.ProductCategorys.Remove(cate);
			_db.SaveChanges();
		}
	}
}
