using BeSpokedBikeSales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeSpokedBikeSales.Interface
{
    public interface ISalesService
    {
        public List<Sale> GetListOfSales();
        public Sale? GetSaleById(int id);
        public Sale CreateSale(Sale sale);
        public Sale UpdateSale(Sale sale);
        public Sale DeleteSale(int id);
    }
}
