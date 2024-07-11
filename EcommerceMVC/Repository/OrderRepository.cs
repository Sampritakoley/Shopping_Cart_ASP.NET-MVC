using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.ViewModel;

namespace EcommerceMVC.Repository
{
	public class OrderRepository
	{
		private readonly AppDbContext _db;
		public OrderRepository(AppDbContext db)
		{
			_db = db;
		}
		
		public OrderDetail CreateOrder(User user, OrderViewModel model)
		{
			var order = new OrderDetail()
			{
				Email = user.Email,
				OrderDate = model.OrderDate,
				DeliveryDate = model.DeliveryDate,
				OrderPrice = model.OrderPrice,
				UserId = user.Id,
			};
			_db.OrderDetails.Add(order);
			_db.SaveChanges();
			foreach (var item in model.CartItems)
			{
				var orderitem = new OrderItem()
				{
					Name = item.Name,
					price = item.price,
					Quantity = item.Quantity,
					OrderId=order.Id
				};
				_db.OrderItems.Add(orderitem);
				_db.SaveChanges();
				var product=_db.Products.Where(p=>p.Name==orderitem.Name).FirstOrDefault();
				product.NoOfProduct = product.NoOfProduct - orderitem.Quantity;
				_db.SaveChanges();
			}
			return order;
		}

		public IEnumerable<Order> GetOrderBook(int userid)
		{
			var orderlist = _db.Orders.Where(x => x.UserId == userid).ToList();
			return orderlist;
		}

	}
}
