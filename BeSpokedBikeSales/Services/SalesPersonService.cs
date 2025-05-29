using BeSpokedBikeSales.Data;
using BeSpokedBikeSales.Interface;
using BeSpokedBikeSales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BeSpokedBikeSales.Services
{
    public class SalesPersonService : ISalesPersonService
    {
        private BeSpokedBikeSalesContext _context;

        public SalesPersonService(BeSpokedBikeSalesContext context)
        {
            _context = context;
        }

        public List<SalesPerson> GetListOfSalesPeople()
        {
            return _context.SalesPerson.ToList();
        }

        public SalesPerson CreateSalesPerson(SalesPerson salesPerson)
        {
            if (salesPerson?.FirstName == null || salesPerson.LastName == null || CheckDuplicateExsist(salesPerson))
            {
                throw new Exception("Insuficient Data or Duplicate");
            }
            
            _context.SalesPerson.AddAsync(salesPerson);
            _context.SaveChanges();
            return salesPerson;
        }

        public SalesPerson DeleteSalesPerson(int id)
        {
            var salesPerson = GetSalesPersonById(id);
            if (salesPerson != null)
            {
                _context.SalesPerson.Remove(salesPerson);
                _context.SaveChanges();
            }
            return salesPerson;
        }


        public SalesPerson? GetSalesPersonById(int id)
        {
            return  _context.SalesPerson.FirstOrDefault(x => x.SalesPersonId == id);
        }

        public SalesPerson UpdateSalesPerson(SalesPerson salesPerson)
        {
            _context.SalesPerson.Update(salesPerson);
            _context.SaveChanges();
            return salesPerson;
        }

        bool CheckDuplicateExsist(SalesPerson salesPerson)
        {
            var firstNameMatch = _context.SalesPerson.FirstOrDefault(f => f.FirstName == salesPerson.FirstName);
            var secondNameMatch = _context.SalesPerson.FirstOrDefault(s => s.LastName == salesPerson.LastName);

            return firstNameMatch != null && secondNameMatch != null;
        }

        public SelectList GetSalesPeopleSelectList()
        {
            var salesPeople = _context.SalesPerson
                .AsNoTracking()
                .ToList();
            SelectList selectListItems = new SelectList(salesPeople, "SalesPersonId", "FirstName");

            return selectListItems;
        }
    }



}
