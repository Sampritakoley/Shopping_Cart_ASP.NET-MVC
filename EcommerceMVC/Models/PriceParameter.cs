using StackExchange.Redis;

namespace EcommerceMVC.Models
{
    public class PriceParameter
    {
        public uint PriceRange { get; set; }

        public uint MaxPrice { get; set; } = (uint)10000;

        public bool ValidRange(uint priceRange)
        {
            if(priceRange<0|| priceRange > MaxPrice)
            {
                return false;
            }
            return true;
        }
    }
}
