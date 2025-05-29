using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikeSales.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [DisplayName("Product")]
        public string Name { get; set; }
        public string Manufacturer { get; set; } = "BeSpoked";
        public string? Style { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [DataType(DataType.Currency)]
        public decimal PurchasePrice { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }
        public int QuantityOnHand { get; set; }
        [Range(0, 100)]
        public int CommisionPercentage { get; set; }
    }
}
