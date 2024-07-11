using EcommerceMVC.Data;
using EcommerceMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
	public class OrderController : Controller
	{
		private readonly AppDbContext _db;
		
		public OrderController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult OrderCreate()
		{
			int paymentid =Convert.ToInt16(TempData["paymentid"]);
			TempData.Keep("paymentid");
			int userid = Convert.ToInt16(TempData["userId"]);
			TempData.Keep("userId");
			var order = new Order()
			{
				OrderDate=DateTime.Now,
				OrderStatus="Confirmed",
				Paymentid= paymentid,
				UserId=userid,
			};
			_db.Orders.Add(order);
			_db.SaveChanges();
			return View(order);
		}
	}
}
