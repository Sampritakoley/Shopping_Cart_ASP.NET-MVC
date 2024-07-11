using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.Services;
using EcommerceMVC.ViewModel;
using System.Text.RegularExpressions;

namespace EcommerceMVC.Repository
{
	public class UserRepository
	{
		private readonly AppDbContext _db;

		public UserRepository(AppDbContext db)
		{
			_db = db;
		}
		public bool SearchByEmail(string email)
		{
			var res = _db.Users.Where(e => e.Email == email).FirstOrDefault();
			if (res == null) return false;
			return true;
		}
		public User RegisterAccount(UserViewModel user)
		{
			var newUser = new User()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				GenderType = user.GenderType,
				DateOfBirth = user.DateOfBirth,
				Password = Password.EncryptPassword(user.Password),
				RoleCategory = user.RoleCategory
			};

			var newAddress = new Address()
			{
				Street = user.Address.Street,
				State = user.Address.State,
				City = user.Address.City,
				PostalCode = user.Address.PostalCode,
				Country = user.Address.Country,
				UserId=newUser.Id
			};
			newUser.Addresses.Add(newAddress);
			_db.Users.Add(newUser);
			_db.SaveChanges();
			return newUser;
		}
		public bool isValid(string email)
		{
			try
			{
				var mail = new System.Net.Mail.MailAddress(email);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool isPasswordValid(string password)
		{
			Regex regexObj = new Regex(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{2,})$");
			bool valid = regexObj.IsMatch(password);
			if (valid && password.Length <= 15)
			{
				return true;
			}
			return false;
		}

		public User? GetById(int id)
		{
			return _db.Users.Where(u => u.Id == id).FirstOrDefault();
		}
	}
}
