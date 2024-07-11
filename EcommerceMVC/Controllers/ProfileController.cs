using Dapper;
using EcommerceMVC.Models;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EcommerceMVC.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index(ProfileViewModel model)
        {
            return View(model);
        }
        [HttpGet]
        public IActionResult GetProfileBYId()
        {
            int id = Convert.ToUInt16(TempData["userId"]);
            TempData.Keep("userId");
            using (var connection = new SqlConnection("Data Source=DESKTOP-IQB900E\\SQLEXPRESS01;Initial Catalog=EcommerceMVCdb;Integrated Security=True;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"))
            {
                connection.Open();
                var resultUser = connection.Query<User>("SELECT * FROM dbo.fn_FindById(@id)", new { id }).FirstOrDefault();
                connection.Close();
                var model = new ProfileViewModel()
                {
                    FirstName = resultUser.FirstName,
                    LastName = resultUser.LastName,
                    Email = resultUser.Email,
                    GenderType = Convert.ToString(resultUser.GenderType),
                    DateOfBirth = resultUser.DateOfBirth
                };
                return RedirectToAction("Index",model);
            }
        }
    }
}
