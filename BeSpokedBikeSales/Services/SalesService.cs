using BeSpokedBikeSales.Data;
using BeSpokedBikeSales.Interface;
using BeSpokedBikeSales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikeSales.Services
{
    public class SalesService : ISalesService
    {
        private BeSpokedBikeSalesContext _context;
        IProductsService _productsService;

        public SalesService(BeSpokedBikeSalesContext context, IProductsService productsService) 
        { 
            _context = context; 
            _productsService = productsService;
        }

        public Sale CreateSale(Sale sale)
        {
            try
            {
                var today = DateTime.UtcNow.Date;
                var product = _productsService.GetProductById(sale.ProductId);

                if (product == null)
                    throw new InvalidDataException("Product Not Found");

                var discount = _context.Discount.FirstOrDefault(d => d.ProductId == product.ProductId && d.BeginDate <= today
                            && d.EndDate >= today);
                sale = SetSaleValues(sale, product, discount?.DiscountPercentage ?? 0);
                _context.Sale.Add(sale);
                _context.SaveChanges();
                return sale;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Sale DeleteSale(int id)
        {
            try
            {
                var sale = _context.Sale.FirstOrDefault(x => x.SaleId == id);
                if (sale != null)
                {
                    _context.Sale.Remove(sale);
                    _context.SaveChanges();
                }
                return sale;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Sale> GetListOfSales()
        {
            try
            {
                var sales = _context.Sale
                    .Include(c => c.Customer)
                    .Include(p => p.Product)
                    .Include(s => s.SalesPerson)
                    .ToList();

                return sales;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Sale> GetFilteredListOfSales(DateTime startDate, DateTime endDate)
        {
            try
            {
                var sales = _context.Sale
                    .Include(c => c.Customer)
                    .Include(p => p.Product)
                    .Include(s => s.SalesPerson)
                    .Where(s => s.SalesDate >= startDate && s.SalesDate <= endDate)
                    .ToList();
                return sales;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Sale? GetSaleById(int id)
        {
            try
            {
                return _context.Sale.FirstOrDefault(x => x.SaleId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Sale UpdateSale(Sale sale)
        {
            try
            {
                var today = DateTime.UtcNow.Date;
                var product = _productsService.GetProductById(sale.ProductId);

                if (product == null)
                    throw new InvalidDataException("Product Not Found");

                var discount = _context.Discount.FirstOrDefault(d => d.ProductId == product.ProductId && d.BeginDate < today
                            && d.EndDate > today);

                sale = SetSaleValues(sale, product, discount?.DiscountPercentage ?? 0);
                _context.Sale.Update(sale);
                _context.SaveChanges();
                return sale;
            }
            catch (Exception)
            {

                throw;
            }
        }

        Sale SetSaleValues(Sale sale, Product product, decimal discount)
        {

            sale.Price = GetDiscountedPrice(product.SalePrice, discount);
            sale.SaleCommission = ((decimal)product.CommisionPercentage / 100) * sale.Price;
            return sale;
        }

        decimal GetDiscountedPrice(decimal price, decimal discountAmount)
        {
            return price - (price * (discountAmount/100));
        }
    }
}
