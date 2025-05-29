using BeSpokedBikeSales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeSpokedBikeSales.Interface
{
    public interface ISalesPersonService
    {
        public List<SalesPerson> GetListOfSalesPeople();
        public SalesPerson? GetSalesPersonById(int id);
        public SalesPerson CreateSalesPerson(SalesPerson salesPerson);
        public SalesPerson UpdateSalesPerson(SalesPerson salesPerson);
        public SalesPerson DeleteSalesPerson(int id);
        public SelectList GetSalesPeopleSelectList();

    }
}
