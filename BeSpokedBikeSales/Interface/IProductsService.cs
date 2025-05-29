using BeSpokedBikeSales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeSpokedBikeSales.Interface
{
    public interface IProductsService
    {
        public List<Product> GetListOfProducts();
        public Product? GetProductById(int Id);
        public Product CreateProduct(Product product);
        public Product UpdateProduct(Product product);
        public Product DeleteProduct(int Id);
        public SelectList GetProductSelectList();
    }
}
