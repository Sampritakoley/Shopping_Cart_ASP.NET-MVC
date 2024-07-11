using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressRepository addressRepository;
        
        public AddressController(AddressRepository _addressRepository)
        {
            addressRepository = _addressRepository;
        }
        public IActionResult Index()
        {
            int userid = Convert.ToUInt16(TempData["userId"]);
            TempData.Keep("userId");
            IEnumerable<Address> addressList = addressRepository.GetAllAddressList(userid);   //model
            return View(addressList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddressViewModel model)
        {
            int userid = Convert.ToUInt16(TempData["userId"]);
            TempData.Keep("userId");
            var address = addressRepository.CreateAddress(model, userid);
            return RedirectToAction("Index", "Address");
        }
    }
}
