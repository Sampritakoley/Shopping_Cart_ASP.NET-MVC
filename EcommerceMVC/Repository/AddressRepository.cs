using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.ViewModel;

namespace EcommerceMVC.Repository
{
    public class AddressRepository
    {
        private readonly AppDbContext _dbcontext;

        public AddressRepository(AppDbContext dbcontexyt)
        {
            _dbcontext = dbcontexyt;
        }

        public IEnumerable<Address> GetAllAddressList(int userid)
        {
                var addressList = _dbcontext.Addresses.Where(x => x.UserId == userid).ToList();
                return addressList;
        }

        public Address CreateAddress(AddressViewModel address, int userid)
        {
            Address newAddress = new Address()
            {
                Street = address.Street,
                State = address.State,
                City = address.City,
                PostalCode = address.PostalCode,
                Country = address.Country,
                UserId = userid,
            };
            _dbcontext.Addresses.Add(newAddress);
            _dbcontext.SaveChanges();
            return newAddress;
        }
    }
}
