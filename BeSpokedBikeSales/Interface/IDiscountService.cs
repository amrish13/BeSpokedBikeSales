using BeSpokedBikeSales.Models;

namespace BeSpokedBikeSales.Interface
{
    public interface IDiscountService
    {
        public List<Discount> GetListOfDiscounts();
        public Discount? GetDiscountById(int Id);
        public Discount CreateDiscount(Discount discount);
        public Discount UpdateDiscount(Discount discount);
        public Discount DeleteDiscount(int Id);
    }
}
