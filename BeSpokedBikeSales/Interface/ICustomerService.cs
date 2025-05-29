using BeSpokedBikeSales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeSpokedBikeSales.Interface
{
    public interface ICustomerService
    {
        public List<Customer> GetListOfCustomers();
        public Customer? GetCustomerById(int id);
        public Customer CreateCustomer(Customer customer);
        public Customer UpdateCustomer(Customer customer);
        public Customer DeleteCustomer(int id);
        public bool CheckDuplicateExsist(Customer customer);
        public SelectList GetCustomerSelectList();
    }
}
