using BeSpokedBikeSales.Data;
using BeSpokedBikeSales.Interface;
using BeSpokedBikeSales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikeSales.Services
{
    public class ProductsService : IProductsService
    {
        private BeSpokedBikeSalesContext _context;
        public ProductsService(BeSpokedBikeSalesContext context) { _context = context; }

        public Product CreateProduct(Product product)
        {
            try
            {
                if (CheckDuplicateExsist(product))
                {
                    throw new InvalidOperationException("Duplicate Product Exsist.");
                }
                else
                {
                    _context.Product.Add(product);
                    _context.SaveChanges();
                    return product; 
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Product DeleteProduct(int Id)
        {
            try
            {
                var product = _context.Product.FirstOrDefault(x => x.ProductId == Id);
                if (product != null)
                {
                    _context.Product.Remove(product);
                    _context.SaveChanges(); 
                }
                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Product> GetListOfProducts()
        {
            try
            {
                return _context.Product.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Product? GetProductById(int Id)
        {
            try
            {
                return _context.Product.FirstOrDefault(x => x.ProductId == Id);
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Product UpdateProduct(Product product)
        {
            try
            {
                _context.Product.Update(product);
                _context.SaveChanges();
                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckDuplicateExsist(Product product)
        {
            var match = _context.Product.FirstOrDefault(f => f.Name == product.Name);

            return match != null;
        }

        public SelectList GetProductSelectList()
        {
            var products = _context.Product
                .AsNoTracking()
                .ToList();
            SelectList selectListItems = new SelectList(products, "ProductId", "Name");

            return selectListItems;
        }
    }
}
