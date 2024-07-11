using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using Stripe;

namespace EcommerceMVC.Controllers
{
	public class PaymentController : Controller
	{
		private IConfiguration _config;
		private readonly AppDbContext _db;

		private readonly UserRepository _userRepo;

		public PaymentController(IConfiguration config,AppDbContext db,UserRepository userRepo)
		{
			_config = config;
			_db = db;
			_userRepo = userRepo;
		}
		public IActionResult Index(int amount)
		{
			int Total=amount;
			TempData["amt"] = amount;
			TempData.Keep("amt");
			var Key = _config["RazoePayKey"].ToString();
			var secret = _config["RazorPatSecret"].ToString();
			RazorpayClient client=new RazorpayClient(Key, secret);
			Dictionary<string,Object> option = new Dictionary<string,Object>();
			option.Add("amount", Convert.ToDecimal(Total));
			option.Add("currency", "USD");
            Razorpay.Api.Order odr = client.Order.Create(option);
			ViewBag.orderId = odr["id"].ToString();
			return View("Charge");
		}
		//[HttpPost]
		public IActionResult Charge(string razorpay_payment_id, string razorpay_order_id,string razorpay_signature)
		{
			Dictionary<string,string> attribute=new Dictionary<string,string>();
			attribute.Add("razorpay_payment_id", razorpay_payment_id);
			attribute.Add("razorpay_order_id", razorpay_order_id);
			attribute.Add("razorpay_signature", razorpay_signature);
			int userid = Convert.ToInt16(TempData["userId"]);
			TempData.Keep("userId");
			var resUser = _userRepo.GetById(userid);
			var payment = new PaymentModel()
			{
				TransactionId = razorpay_payment_id,
				Status = "Paid",
				UserId=userid,
			};
			try
			{
				Utils.verifyPaymentSignature(attribute);
				resUser.NoOfTransaction += 1;
				_db.PaymentModels.Add(payment);
				_db.SaveChanges();
				TempData["paymentid"] = payment.Id;
				return View("Success", payment);
			}
			catch (Exception ex)
			{
				return View("Failure");
			}
			return View();
		}
	}
}
