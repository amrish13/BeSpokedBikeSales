using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikeSales.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int SalesPersonId { get; set; }
        public int CustomerId { get; set; }
        [DataType(DataType.Date)]
        public DateTime SalesDate { get; set; } = DateTime.UtcNow.Date;
        [Column(TypeName = "decimal(10, 2)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [DataType(DataType.Currency)]
        [DisplayName("Commission")]
        public decimal SaleCommission { get; set; }
        public Product? Product { get; set; }
        public SalesPerson? SalesPerson { get; set; }
        public Customer? Customer { get; set; }
    }
}
