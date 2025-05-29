using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikeSales.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; } = DateTime.UtcNow.Date;

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.UtcNow.Date;

        [Range(0, 100)]
        public int DiscountPercentage { get; set; }
    }
}
