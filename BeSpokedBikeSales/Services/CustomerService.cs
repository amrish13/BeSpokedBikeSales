using BeSpokedBikeSales.Data;
using BeSpokedBikeSales.Interface;
using BeSpokedBikeSales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikeSales.Services
{
    public class CustomerService : ICustomerService
    {
        private BeSpokedBikeSalesContext _context;
        public CustomerService(BeSpokedBikeSalesContext context)
        {
            _context = context;
        }

        public bool CheckDuplicateExsist(Customer customer)
        {
            var firstNameMatch = _context.Customer.FirstOrDefault(c =>  c.FirstName == customer.FirstName);
            var lastNameMatch = _context.Customer.FirstOrDefault(c => c.LastName == customer.LastName);

            return firstNameMatch != null && lastNameMatch != null;
        }

        public Customer CreateCustomer(Customer customer)
        {
            if (customer?.FirstName == null || customer?.LastName == null || CheckDuplicateExsist(customer))
            {
                throw new Exception("Insuficient Data or Duplicate");
            }
            _context.Customer.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer DeleteCustomer(int id)
        {
            var customer = GetCustomerById(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
                _context.SaveChanges();
            }
            return customer;
        }

        public Customer? GetCustomerById(int id)
        {
            return _context.Customer.FirstOrDefault(x => x.CustomerId == id);
        }

        public List<Customer> GetListOfCustomers()
        {
            return _context.Customer.ToList();
        }

        public Customer UpdateCustomer(Customer customer)
        {
            _context.Customer.Update(customer);
            _context.SaveChanges();
            return customer;
        }

        public SelectList GetCustomerSelectList()
        {
            var customers = _context.Customer
                .AsNoTracking()
                .ToList();
            SelectList selectListItems = new SelectList(customers, "CustomerId", "FirstName");

            return selectListItems;
        }
    }
}
