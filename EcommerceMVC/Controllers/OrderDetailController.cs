using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using EcommerceMVC.Services;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
	public class OrderDetailController : Controller
	{
		private readonly UserRepository _userRepository;
		private readonly OrderRepository _orderRepository;
		public OrderDetailController(UserRepository userRepository,OrderRepository orderRepository)
		{
			_userRepository = userRepository;
			_orderRepository = orderRepository;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult PlaceOrder()
		{
			var value = TempData.Peek<List<CartItem>>("listofCartItem");
			int userid = Convert.ToInt16(TempData["userId"]);
			TempData.Keep("userId");
			var user=_userRepository.GetById(userid);
			int totalprice = 0;
			foreach(var item in value)
			{
				if(item.IsOUtOfStock=="True")
				{
					ViewBag.OutOfStock = "Remove Unavailable Products";
					return RedirectToAction("Index", "CartItem");
				}
				totalprice = totalprice + item.price;
			}
			var model = new OrderViewModel()
			{
				CartItems = value,
				OrderPrice = totalprice,
				DeliveryDate = DateTime.Now.AddDays(5),
				OrderDate = DateTime.Now,
				Status = "Confirmed",
				Email = user.Email,
			};
			var order=_orderRepository.CreateOrder(user, model);
			TempData["orderid"] = order.Id;
			TempData.Keep("orderid");
			ViewBag.username=user.FirstName+" "+ user.LastName;
			ViewBag.email=user.Email;
			ViewBag.contactNo = user.PhoneNumber;
			return View(order);
		}
		
		
	}
}
