namespace BeSpokedBikeSales.Models
{
    public class QuaterlyCommissionReport
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public decimal TotalCommission { get; set; }
        public int TotalSales { get; set; }
    }
}
