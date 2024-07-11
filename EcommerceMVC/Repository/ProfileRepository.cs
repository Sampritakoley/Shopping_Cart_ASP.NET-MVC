using EcommerceMVC.Data;
using EcommerceMVC.Models;
using EcommerceMVC.Services;

namespace EcommerceMVC.Repository
{
    public class ProfileRepository
    {
        private readonly AppDbContext _context;
        private readonly Photo _photo;
        public ProfileRepository(AppDbContext context,Photo photo)
        {
            _context = context;
            _photo= photo;
        }
        public async Task ProfileAdd(Profile pro)
        {
            //var result = await _photo.AddPhoto(pro);
            _context.Profiles.Add(pro);
            _context.SaveChanges();
        }
    }
}
