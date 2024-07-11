using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using EcommerceMVC.Services;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Principal;

namespace EcommerceMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserRepository _userRepository;
		private readonly ProfileRepository _proRepo;
		private readonly AppDbContext _db; 
		public AccountController(UserRepository userRepository,AppDbContext db,ProfileRepository proRepo)
		{
			_userRepository = userRepository;
			_db = db;
			_proRepo= proRepo;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserViewModel user)
		{
			if(user==null)
			{
				ViewBag.Message = "No Information is found to register";
				return View(user);
			}
			if(!_userRepository.isValid(user.Email))
			{
				ViewBag.Message = "Email Address is not Valid";
				return View(user);
			}
			if(!_userRepository.isPasswordValid(user.Password))
			{
				ViewBag.Message = "Set A strong Alphanumeric Password";
				return View(user);
			}
			if(_userRepository.SearchByEmail(user.Email))
			{
				ViewBag.Message = "Username is already exist";
				return View(user);
			}
			if (ModelState.IsValid)
			{
				var newuser=_userRepository.RegisterAccount(user);
				var newprofile = new Profile()
				{
					FirstName = newuser.FirstName,
					LastName = newuser.LastName,
					Email = newuser.Email,
					PhoneNumber=newuser.PhoneNumber,
					GenderType=Convert.ToString(newuser.GenderType),
					DateOfBirth=newuser.DateOfBirth,
					UserId=newuser.Id
				};
				_proRepo.ProfileAdd(newprofile);
                ViewBag.LoginMsg = "Now you can login";
				TempData["RegisterSuccess"]= "You have been registered successfully!  Now Login";
				return RedirectToAction("Login", "Account");
			}
			return View(user);
		}

		public IActionResult Login()
		{
			ClaimsPrincipal claimuser = HttpContext.User;
			ViewBag.LoginMessage = TempData["RegisterSuccess"];
			if(claimuser.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Error = "User need to enter username and password to login";
				return RedirectToAction("Login");
			}
			var user=_db.Users.Where(u=>u.Email==model.UserName).FirstOrDefault();
			if(model.UserName==user.Email && Password.EncryptPassword(model.Password)==user.Password)
			{
				var claims = new List<Claim>
			    {
				      new Claim(ClaimTypes.Name, model.UserName),
				      new Claim(ClaimTypes.Role, Convert.ToString(user.RoleCategory))
			    };
				ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				AuthenticationProperties property = new AuthenticationProperties()
				{
					AllowRefresh = true,
					IsPersistent = model.KeepMeLoggedIn
				};
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),property);
				TempData["userId"] = user.Id;
				TempData.Keep("userId");
				return RedirectToAction("Index");
			}
			ViewBag.Error = "Invalid Username Password";
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}
	}
}
