using System.ComponentModel.DataAnnotations;

namespace BeSpokedBikeSales.Models
{
    public class SalesPerson
    {
        public int SalesPersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        [DataType(DataType.Date)]
        public DateTime? TerminationDate { get; set; }
        public string Manager { get; set; }
    }
}
