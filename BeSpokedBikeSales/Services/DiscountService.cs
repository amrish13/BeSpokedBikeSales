using BeSpokedBikeSales.Data;
using BeSpokedBikeSales.Interface;
using BeSpokedBikeSales.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikeSales.Services
{
    public class DiscountService : IDiscountService
    {
        private BeSpokedBikeSalesContext _context;
        public DiscountService(BeSpokedBikeSalesContext context) { _context = context; }

        public Discount CreateDiscount(Discount discount)
        {
            try
            {
                _context.Discount.Add(discount);
                _context.SaveChanges();
                return discount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Discount DeleteDiscount(int Id)
        {
            try
            {
                var discount = _context.Discount.FirstOrDefault(d => d.DiscountId == Id);
                if (discount != null)
                {
                    _context.Discount.Remove(discount);
                    _context.SaveChanges();
                }
                return discount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Discount? GetDiscountById(int id)
        {
            try
            {
                return _context.Discount
                    .Include(p => p.Product)
                    .FirstOrDefault(d => d.DiscountId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Discount> GetListOfDiscounts()
        {
            var discounts = _context.Discount
                .Include(p => p.Product)
                .Where(d => d.EndDate >= DateTime.UtcNow.Date)
                .ToList();
            return discounts;
        }

        public Discount UpdateDiscount(Discount discount)
        {
            try
            {
                _context.Discount.Update(discount);
                return discount;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
